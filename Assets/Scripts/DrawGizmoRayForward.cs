using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoRayForward : MonoBehaviour
{
    [SerializeField] float totalFOV = 60.0f;
    [SerializeField] float rayRange = 10.0f;
    [SerializeField] bool alwaysShowInEditor = false;

    void OnDrawGizmosSelected()
    {
        if (!alwaysShowInEditor)
        {
            Gizmos.color = Color.red;
            Vector3 direction = transform.TransformDirection(Vector3.forward) * rayRange;
            Gizmos.DrawRay(transform.position, direction);

            float halfFOV = totalFOV / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-totalFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(totalFOV, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
            Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
        }
    }

    void OnDrawGizmos()
    {
        if (alwaysShowInEditor) {
            Gizmos.color = Color.red;
            Vector3 direction = transform.TransformDirection(Vector3.forward) * rayRange;
            Gizmos.DrawRay(transform.position, direction);

            float halfFOV = totalFOV / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-totalFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(totalFOV, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
            Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
        }
    }
}
