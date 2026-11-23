using UnityEngine;

/// <summary>
/// Toggles particle system
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ToggleParticle : MonoBehaviour
{
    private ParticleSystem currentParticleSystem = null;
    private MonoBehaviour currentOwner = null;

    public ParticleSystem waterParticles;
    public float cooldown = 0.5f;

    private bool isFlowing = false;
    private float lastToggleTime = -1f;

    private void Awake()
    {
        currentParticleSystem = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        currentParticleSystem.Play();
    }

    public void Stop()
    {
        currentParticleSystem.Stop();
    }

    public void PlayWithExclusivity(MonoBehaviour owner)
    {
        if(currentOwner == null)
        {
            currentOwner = this;
            Play();
        }
    }

    public void StopWithExclusivity(MonoBehaviour owner)
    {
        if(currentOwner == this)
        {
            currentOwner = null;
            Stop();
        }
    }

    private void OnValidate()
    {
        if(currentParticleSystem)
        {
            ParticleSystem.MainModule main = currentParticleSystem.main;
            main.playOnAwake = false;
        }
    }

    public void Toggle()
    {
        if (Time.time - lastToggleTime < cooldown)
            return;

        lastToggleTime = Time.time;
        isFlowing = !isFlowing;

        if (isFlowing)
            waterParticles.Play();
        else
            waterParticles.Stop();
    }
}
