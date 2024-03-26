using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [Range(0,100)]
    public float health = 100;

    [Header("Spring Question")]

    [Range(1,100)]
    public float turns = 2;
    public float radius = 2;
    public float bigRadius = 0;

    private void OnDrawGizmos()
    {
        float greenColor = Mathf.InverseLerp(0, 100, health);
        float redColor = Mathf.InverseLerp(100, 0, health);
        
        Color color = new Color(redColor, greenColor, 0, 1);
        Handles.color = color;

        Handles.DrawLine(transform.position, transform.position + Vector3.right * health, 10f);

        int detail = 3;
        List<Vector3> pointsArray = new List<Vector3>();
        List<Matrix4x4> matrixes = new List<Matrix4x4>();
        int index = 0;
        Vector3 prev_Point2 = transform.position + bigRadius * (AngleToDirectionXZ(0));
        for(int i = 0; i <= turns * 360; i = i + detail)
        {
            pointsArray.Add(transform.position + bigRadius * (AngleToDirectionXZ(i/turns)));
            matrixes.Add(Matrix4x4.TRS(transform.position, Quaternion.Euler(0,i/turns,0), new Vector3(1, 1, 1)));
            Handles.color = Color.cyan;
            Handles.DrawLine(prev_Point2, pointsArray[index], 3f);
            prev_Point2 = pointsArray[index];
            index++;
        }

        Matrix4x4 oldGizmoMatrix = Gizmos.matrix;
        List<Vector3> pointsArray2 = new List<Vector3>();
        Vector3 prev_Point = pointsArray[0] + radius * (AngleToDirection(0));
        index = 0;
        for(int i = 0; i <= turns * 360 ; i = i + detail)
        {
            Gizmos.matrix = matrixes[index];
            float num1 = i / detail;
            float num2 = (turns * 360) / detail;
            float divided = num1 / num2;
            //Vector3 new_Point = pointsArray[index] + radius * (AngleToDirection(i) + new Vector3(0, 0, divided * bigRadius));
            Vector3 new_Point = pointsArray[0] + radius * (AngleToDirection(i));
            pointsArray2.Add(Matrix4x4.Inverse(matrixes[index]) * new_Point);
            //Debug.Log(Matrix4x4.Inverse(matrixes[index]) * new_Point);
            //Gizmos.color = Color.black;
            //Gizmos.DrawSphere(new_Point, 0.1f);
            //Handles.DrawLine(prev_Point, new_Point, 3f);

            prev_Point = new_Point;
            index++;
        }

        Handles.matrix = oldGizmoMatrix;

        Vector3 prev_Point3 = pointsArray2[0];
        for(int i = 1; i < pointsArray2.Count; i++)
        {
            float greenColor2 = Mathf.InverseLerp(1, pointsArray2.Count, i);
            float redColor2 = Mathf.InverseLerp(pointsArray2.Count, 1, i);
            Color color2 = new Color(redColor2, greenColor2, 0, 1);
            Handles.color = color2;
            Handles.DrawLine(prev_Point3, pointsArray2[i], 3f);
            prev_Point3 = pointsArray2[i];
        }
    }

    Vector3 AngleToDirection(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
    }

    Vector3 AngleToDirectionXZ(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
