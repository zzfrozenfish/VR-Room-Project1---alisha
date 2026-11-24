using UnityEngine;

public class StoveBurner : MonoBehaviour
{
    public ParticleSystem burnerFlames;
    public AudioSource flameAudio;
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
            isLit = false;
            burnerFlames.Stop();
            if (flameAudio != null) flameAudio.Stop();
        }
        else if (nearbyLighterFlame != null && nearbyLighterFlame.isPlaying)
        {
            isLit = true;
            burnerFlames.Play();
            if (flameAudio != null) flameAudio.Play();
        }
    }
}