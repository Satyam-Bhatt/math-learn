using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleRenders : MonoBehaviour
{
/*    public float radius = 1f;
    public float radius2 = 1f;*/
    public float bigRadius = 2f;
   // public float displaceAngle = 0f;
    //public float anngle_ = 0f;

    public 
        List<float> radiuses = new List<float>();

    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, Vector3.up, bigRadius);
        
        Handles.color = Color.green;
        Handles.DrawWireDisc(AngleToDirection(0) * bigRadius, Vector3.up, radiuses[0]);
        float firstAngle = Mathf.Asin(radiuses[0] / (2 * bigRadius)) * Mathf.Rad2Deg * 2;
        float anngle_ = 0f;
        for (int i = 1; i < radiuses.Count; i++)
        {
            float secondAngle = Mathf.Asin(radiuses[i] / (2 * bigRadius)) * Mathf.Rad2Deg * 2;

            Vector3 locationOfSecondCircle = (AngleToDirection(anngle_+ secondAngle + firstAngle)) * bigRadius;
            Vector3 locationOfFirstCircle = (AngleToDirection(anngle_)) * bigRadius;

            float dist = Vector3.Distance(locationOfFirstCircle, locationOfSecondCircle);

            Vector3 r1Tor2 = locationOfSecondCircle - locationOfFirstCircle;
            Vector3 centerTOr1 = transform.position - locationOfFirstCircle;

            float phi = Vector3.Angle(r1Tor2, centerTOr1);

            float tanAlpha1 = Mathf.Sin(phi * Mathf.Deg2Rad) / ((bigRadius / (dist - radiuses[i]) - Mathf.Cos(phi * Mathf.Deg2Rad)));
            float tanAlpha2 = Mathf.Sin(phi * Mathf.Deg2Rad) / ((bigRadius / (dist - radiuses[i - 1]) - Mathf.Cos(phi * Mathf.Deg2Rad)));

            float tanAlpha1_Value = Mathf.Atan(tanAlpha1) * Mathf.Rad2Deg;
            float tanAlpha2_Value = Mathf.Atan(tanAlpha2) * Mathf.Rad2Deg;

            float theta = 180 - 2 * phi - tanAlpha1_Value - tanAlpha2_Value;

            locationOfSecondCircle = (AngleToDirection(anngle_ + secondAngle + firstAngle + theta)) * bigRadius;

            Handles.DrawWireDisc(locationOfSecondCircle, Vector3.up, radiuses[i]);

            anngle_ = anngle_ + firstAngle + secondAngle + theta;
            firstAngle = secondAngle;
        }


    }

    private Vector3 AngleToDirection(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) , 0, Mathf.Sin(Mathf.Deg2Rad * angle));
    }
}
