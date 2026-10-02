using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 90, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * 360 * Time.deltaTime);
    }
}
