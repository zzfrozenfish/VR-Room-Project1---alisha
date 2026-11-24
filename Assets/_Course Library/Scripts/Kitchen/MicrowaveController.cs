using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

public class MicrowaveController : MonoBehaviour
{
    private enum State { Idle, Running, Done }
    private State currentState = State.Idle;

    [Header("Display & Window Glow")]
    public TMP_Text timerDisplay;
    public Renderer windowGlowRenderer;
    public Color runningColor = new Color(1f, 0.6f, 0.15f, 0.65f); // Warm orange glow
   

    [Header("Door & Locking")]
    public HingeJoint doorHinge;
    public XRGrabInteractable doorGrab;
    public Rigidbody doorRb;
    public float closedAngleThreshold = 10f;

    [Header("Turntable (Optional)")]
    public Transform turntable;
    public float spinSpeed = 60f;

    [Header("Audio")]
    public AudioSource microwaveAudio;
    public AudioClip dingClip;

    private float timeRemaining = 0f;
    private List<HeatableFood> foodInside = new List<HeatableFood>();

    private void Start()
    {
        SetIdleState();
    }

    // Link each small time button to this method and type 5, 10, 15, etc. in the Inspector
    public void SelectTime(float seconds)
    {
        if (currentState == State.Running) return;

        timeRemaining = seconds;
        currentState = State.Idle;
        UpdateDisplay(timeRemaining);
        SetGlow(false, runningColor);
    }

    // Link the large round start button to this method
    public void StartMicrowave()
    {
        if (currentState == State.Running || timeRemaining <= 0f || !IsDoorClosed()) return;

        currentState = State.Running;
        LockDoor(true);
        SetGlow(true, runningColor);

        if (microwaveAudio != null)
            microwaveAudio.Play();
    }

    private void Update()
    {
        if (currentState == State.Running)
        {
            if (turntable != null)
                turntable.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.Self);

            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                FinishCooking();
            }
            else
            {
                UpdateDisplay(timeRemaining);
            }
        }
        else if (currentState == State.Done)
        {
            // Turn back to normal as soon as the player opens the door
            if (!IsDoorClosed())
            {
                SetIdleState();
            }
        }
    }

    private void FinishCooking()
    {
        timeRemaining = 0f;
        currentState = State.Done;
        UpdateDisplay(0f);
        LockDoor(false);
        

        if (microwaveAudio != null)
        {
            microwaveAudio.Stop(); // Stops the looping hum
            if (dingClip != null)
                microwaveAudio.PlayOneShot(dingClip); // Plays the "ding" once
        }

        foreach (HeatableFood food in foodInside)
        {
            if (food != null) food.HeatUp();
        }
    }

    private void SetIdleState()
    {
        currentState = State.Idle;
        timeRemaining = 0f;
        UpdateDisplay(0f);
        SetGlow(false, runningColor);
    }

    private void LockDoor(bool locked)
    {
        if (doorGrab != null) doorGrab.enabled = !locked;
        if (doorRb != null) doorRb.isKinematic = locked;
    }

    private bool IsDoorClosed()
    {
        if (doorHinge == null) return true;
        return Mathf.Abs(doorHinge.angle) <= closedAngleThreshold;
    }

    private void SetGlow(bool active, Color color)
    {
        if (windowGlowRenderer == null) return;
        windowGlowRenderer.gameObject.SetActive(active);
        if (active) windowGlowRenderer.material.color = color;
    }

    private void UpdateDisplay(float seconds)
    {
        if (timerDisplay == null) return;
        int totalSec = Mathf.CeilToInt(seconds);
        int mins = totalSec / 60;
        int secs = totalSec % 60;
        timerDisplay.text = $"{mins:00}:{secs:00}";
    }

    private void OnTriggerEnter(Collider other)
    {
        HeatableFood food = other.GetComponent<HeatableFood>();
        if (food != null && !foodInside.Contains(food))
            foodInside.Add(food);
    }

    private void OnTriggerExit(Collider other)
    {
        HeatableFood food = other.GetComponent<HeatableFood>();
        if (food != null)
            foodInside.Remove(food);
    }
}