using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineForward : MonoBehaviour
{
    public float lineDistance = 5f;
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * lineDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
