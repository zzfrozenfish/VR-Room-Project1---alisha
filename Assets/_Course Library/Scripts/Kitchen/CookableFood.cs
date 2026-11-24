using UnityEngine;

public class CookableFood : MonoBehaviour
{
    public Renderer foodRenderer;
    public Material cookedMaterial;
    public float cookTime = 4f;

    private float cookTimer = 0f;
    private bool isCooked = false;

    private void OnTriggerStay(Collider other)
    {
        if (isCooked || !other.CompareTag("PotInterior")) return;

        BoilablePot pot = other.GetComponentInParent<BoilablePot>();
        if (pot != null && pot.isBoiling)
        {
            cookTimer += Time.deltaTime;
            if (cookTimer >= cookTime)
            {
                isCooked = true;
                if (foodRenderer != null && cookedMaterial != null)
                {
                    foodRenderer.material = cookedMaterial;
                }
            }
        }
    }
}