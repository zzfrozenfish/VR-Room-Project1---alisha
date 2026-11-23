using UnityEngine;

/// <summary>
/// Set the rotation of an object
/// </summary>
public class RotateObject : MonoBehaviour
{
    [Tooltip("The value at which the speed is applied")]
    [Range(0, 1)] public float sensitivity = 1.0f;

    [Tooltip("The max speed of the rotation")]
    public float speed = 10.0f;

    [Tooltip("Assign the light object here")]
    public GameObject light;

    private bool isRotating = false;

    public void SetIsRotating(bool value)
    {
        if(value)
        {
            Begin();
        }
        else
        {
            End();
        }
    }

    public void Begin()
    {
        isRotating = true;
        UpdateLight();
    }

    public void End()
    {
        isRotating = false;
        UpdateLight();
    }

    public void ToggleRotate()
    {
        isRotating = !isRotating;
        UpdateLight();
    }


    public void SetSpeed(float value)
    {
        sensitivity = Mathf.Clamp(value, 0, 1);
        isRotating = (sensitivity * speed) != 0.0f;
    }

    private void Update()
    {
        if (isRotating)
            Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(transform.up, (sensitivity * speed) * Time.deltaTime);
    }

    void UpdateLight()
    {
        if (light != null)
        {
            light.SetActive(isRotating);
        }
    }
}
