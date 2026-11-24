using UnityEngine;

public class RoastableFood : MonoBehaviour
{
    public GameObject cookedPrefab;

    public void FinishRoasting()
    {
        if (cookedPrefab != null)
        {
            Instantiate(cookedPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}