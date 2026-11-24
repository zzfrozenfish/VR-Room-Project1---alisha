using UnityEngine;

public class ConsumableFood : MonoBehaviour
{
    public AudioSource eatSound; // Optional: assign a crunch/bite sound
    private bool isNearMouth = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouth"))
            isNearMouth = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mouth"))
            isNearMouth = false;
    }

    // Wire this to XRGrabInteractable -> Activated
    public void TryEat()
    {
        if (!isNearMouth) return;

        if (eatSound != null)
            AudioSource.PlayClipAtPoint(eatSound.clip, transform.position);

        Destroy(gameObject);
    }
}