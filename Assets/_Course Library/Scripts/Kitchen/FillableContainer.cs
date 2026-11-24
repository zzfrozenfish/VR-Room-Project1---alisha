using UnityEngine;

public class FillableContainer : MonoBehaviour
{
    [Tooltip("Assign the LiquidPivot parent object here")]
    public Transform liquidPivot;
    public float fillSpeed = 0.01f;

    [Range(0f, 1f)]
    public float fillAmount = 0f;

    private void Start()
    {
        UpdateLiquid();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (fillAmount < 1f)
        {
            fillAmount = Mathf.Clamp01(fillAmount + fillSpeed);
            UpdateLiquid();
        }
    }

    private void UpdateLiquid()
    {
        if (liquidPivot == null) return;

        liquidPivot.gameObject.SetActive(fillAmount > 0.01f);

        Vector3 scale = liquidPivot.localScale;
        scale.y = fillAmount;
        liquidPivot.localScale = scale;
    }
}