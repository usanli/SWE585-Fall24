using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The cube to follow
    public Vector3 offset;   // Offset position from the target

    void LateUpdate()
    {
        // Set the camera's position to be the same as the target's position, plus the offset
        transform.position = target.position + offset;
    }
}
