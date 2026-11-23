using UnityEngine;

public class StoveBurner : MonoBehaviour
{
    public ParticleSystem burnerFlames;
    public bool isLit { get; private set; } = false;

    private ParticleSystem nearbyLighterFlame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LighterFlame"))
        {
            nearbyLighterFlame = other.GetComponent<ParticleSystem>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LighterFlame"))
        {
            nearbyLighterFlame = null;
        }
    }

    public void PressButton()
    {
        if (isLit)
        {
            // Turn off the stove
            isLit = false;
            burnerFlames.Stop();
        }
        else if (nearbyLighterFlame != null && nearbyLighterFlame.isPlaying)
        {
            // Only ignite if the lighter is close AND currently lit
            isLit = true;
            burnerFlames.Play();
        }
    }
}