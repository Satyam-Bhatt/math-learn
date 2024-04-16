using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleRenders : MonoBehaviour
{
    public float radius = 1f;
    public float bigRadius = 2f;
    public float anngle_ = 0f;

    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, Vector3.up, bigRadius);

        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position + (AngleToDirection(anngle_) * bigRadius), Vector3.up, radius);

        Vector3 centerVector = AngleToDirection(anngle_) * bigRadius;

        Vector3 sidePosition = (transform.position + (AngleToDirection(anngle_) * bigRadius) + AngleToDirection(90) * radius);
        //sidePosition.x = bigRadius;
        
        Gizmos.DrawSphere(sidePosition, 0.1f);

        float angleProjected = Mathf.Asin(radius/(2*bigRadius)) * Mathf.Rad2Deg * 4;
        Debug.Log(angleProjected);
        
    }

    private Vector3 AngleToDirection(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) , 0, Mathf.Sin(Mathf.Deg2Rad * angle));
    }
}
