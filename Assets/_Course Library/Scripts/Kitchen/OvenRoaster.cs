using System.Collections.Generic;
using UnityEngine;

public class OvenRoaster : MonoBehaviour
{
    [Header("Window Glow")]
    public GameObject ovenWindowGlow;

    [Header("Roasting Settings")]
    public float roastTime = 5f;
    public bool isOvenOn = false;

    [Header("Audio")]
    public AudioSource ovenAudio;

    private Dictionary<RoastableFood, float> foodTimers = new Dictionary<RoastableFood, float>();



    private void Start()
    {
        UpdateWindowGlow();
    }

    public void ToggleOven()
    {
        isOvenOn = !isOvenOn;
        UpdateWindowGlow();

        if (ovenAudio != null)
        {
            if (isOvenOn) ovenAudio.Play();
            else ovenAudio.Stop();
        }
    }

    private void UpdateWindowGlow()
    {
        if (ovenWindowGlow != null)
            ovenWindowGlow.SetActive(isOvenOn);
    }

    private void OnTriggerEnter(Collider other)
    {
        RoastableFood food = other.GetComponent<RoastableFood>();
        if (food != null && !foodTimers.ContainsKey(food))
            foodTimers.Add(food, 0f);
    }

    private void OnTriggerExit(Collider other)
    {
        RoastableFood food = other.GetComponent<RoastableFood>();
        if (food != null)
            foodTimers.Remove(food);
    }

    private void Update()
    {
        if (!isOvenOn || foodTimers.Count == 0) return;

        var keys = new List<RoastableFood>(foodTimers.Keys);
        foreach (RoastableFood food in keys)
        {
            if (food == null)
            {
                foodTimers.Remove(food);
                continue;
            }

            foodTimers[food] += Time.deltaTime;
            if (foodTimers[food] >= roastTime)
            {
                foodTimers.Remove(food);
                food.FinishRoasting();
            }
        }
    }
}