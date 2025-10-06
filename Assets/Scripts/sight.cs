using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class sight : MonoBehaviour
{
    public float distance;
    public float angle;
    public LayerMask objectsLayers;
    public LayerMask obstaclesLayers;
    public Collider detectedObject;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(
        transform.position, distance, objectsLayers);

        detectedObject = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];

            Vector3 directionToCollider = Vector3.Normalize(
                collider.bounds.center - transform.position);

            float angleToCollider = Vector3.Angle(
                transform.forward, directionToCollider);

            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position,
                collider.bounds.center, out RaycastHit hit, obstaclesLayers))
                {
                    Debug.DrawLine(transform.position,
                    collider.bounds.center, Color.green);
                    detectedObject = collider;
                    break;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

}
