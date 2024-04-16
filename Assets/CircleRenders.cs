using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleRenders : MonoBehaviour
{
    public float radius = 1f;
    public float radius2 = 1f;
    public float bigRadius = 2f;
    public float anngle_ = 0f;

    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, Vector3.up, bigRadius);

        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position + (AngleToDirection(anngle_) * bigRadius), Vector3.up, radius);
        
        float angleProjected = Mathf.Asin(radius/(2*bigRadius)) * Mathf.Rad2Deg * 4;
        Vector3 locationEnd = AngleToDirection(angleProjected/2).normalized * bigRadius;
        
        //Gizmos.DrawSphere(locationEnd, 0.1f);

        float secondAngle = Mathf.Asin(radius2 / (2 * bigRadius)) * Mathf.Rad2Deg * 2;
        Vector3 locationOfSecondCircle = (AngleToDirection(secondAngle + angleProjected / 2)) * bigRadius;

        Handles.DrawWireDisc(locationOfSecondCircle, Vector3.up, radius2);
    }

    private Vector3 AngleToDirection(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) , 0, Mathf.Sin(Mathf.Deg2Rad * angle));
    }
}
