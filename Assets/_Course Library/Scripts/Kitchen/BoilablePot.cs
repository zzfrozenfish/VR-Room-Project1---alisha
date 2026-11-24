using UnityEngine;

public class BoilablePot : MonoBehaviour
{
    public FillableContainer container;
    public ParticleSystem steamParticles;
    public float timeToBoil = 5f;

    public bool isBoiling { get; private set; } = false;
    private float heatTimer = 0f;
    private StoveBurner currentBurner;

    private void OnTriggerEnter(Collider other)
    {
        StoveBurner burner = other.GetComponent<StoveBurner>();
        if (burner != null) currentBurner = burner;
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentBurner != null && other.gameObject == currentBurner.gameObject)
        {
            currentBurner = null;
        }
    }

    private void Update()
    {
        bool heating = currentBurner != null && currentBurner.isLit && container.fillAmount > 0.2f;

        if (heating)
        {
            heatTimer = Mathf.Min(heatTimer + Time.deltaTime, timeToBoil);
        }
        else
        {
            heatTimer = Mathf.Max(heatTimer - Time.deltaTime, 0f);
        }

        bool shouldBoil = heatTimer >= timeToBoil;
        if (shouldBoil != isBoiling)
        {
            isBoiling = shouldBoil;
            if (isBoiling) steamParticles.Play();
            else steamParticles.Stop();
        }
    }
}
