using UnityEngine;

public class OvenDoorLatch : MonoBehaviour
{
    public HingeJoint doorHinge;
    public float snapAngle = 15f;

    private void Update()
    {
        if (doorHinge == null) return;
        // Only engage the closing spring when the door is nearly shut
        doorHinge.useSpring = Mathf.Abs(doorHinge.angle) < snapAngle;
    }
}