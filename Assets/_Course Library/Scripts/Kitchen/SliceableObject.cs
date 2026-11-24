using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    public GameObject slicedPiecePrefab;
    public int pieceCount = 3;
    public float spread = 0.06f;

    private bool isSliced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isSliced || !other.CompareTag("Knife")) return;

        isSliced = true;

        for (int i = 0; i < pieceCount; i++)
        {
            Vector3 offset = new Vector3((i - 1) * spread, 0.02f, 0f);
            Instantiate(slicedPiecePrefab, transform.position + offset, transform.rotation);
        }

        Destroy(gameObject);
    }
}