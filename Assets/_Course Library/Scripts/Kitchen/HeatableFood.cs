using UnityEngine;

public class HeatableFood : MonoBehaviour
{
    public ParticleSystem vapourParticles;
    public float hotDuration = 12f;

    private float coolDownTimer = 0f;
    private bool isHot = false;

    public void HeatUp()
    {
        if (vapourParticles == null) return;

        isHot = true;
        coolDownTimer = hotDuration;
        vapourParticles.Play();
    }

    private void Update()
    {
        if (!isHot) return;

        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer <= 0f)
        {
            isHot = false;
            // Stop emitting new vapour; existing particles will drift up and fade out naturally
            vapourParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}