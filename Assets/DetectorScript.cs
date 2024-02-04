using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    [SerializeField] private Transform ToDetect;

    [Range(0f, 360f)]
    [SerializeField] private float angle = 0f;
    [SerializeField] private float radius = 0f;
    [SerializeField] private float height = 0f;
    [SerializeField] private Shapes shapes;


    private void OnDrawGizmos()
    {
        Vector3 LocalPosition_Ball = transform.InverseTransformPoint(ToDetect.position);

        Handles.matrix = Gizmos.matrix = transform.localToWorldMatrix;

        if(shapes == Shapes.Wedge)
        {
            WedgeShape(LocalPosition_Ball);
        }
        else if (shapes == Shapes.Spherical)
        {
            SphericalShape(LocalPosition_Ball);
        }
        else if (shapes == Shapes.Cone)
        {
            ConeShape(LocalPosition_Ball); 
        }
    }

    private void WedgeShape(Vector3 LocalPosition_Ball)
    {
        Handles.color = Color.yellow;
        Handles.DrawLine(default, height * Vector3.up);

        Vector3 rightPosition = new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), 0, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        Vector3 leftPosition = new Vector3(-radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), 0, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));

        Vector3 rightPosition_Up = new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), height, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        Vector3 leftPosition_Up = new Vector3(-radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), height, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));

        Handles.color = Color.green;
        Handles.DrawWireArc(default, Vector3.up, leftPosition, angle, radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, rightPosition);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, leftPosition);

        Handles.color = Color.green;
        Handles.DrawWireArc(new Vector3(0, height, 0), Vector3.up, leftPosition, angle, radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(height * Vector3.up, rightPosition_Up);

        Gizmos.DrawLine(height * Vector3.up, leftPosition_Up);
        Gizmos.DrawLine(leftPosition_Up, leftPosition);
        Gizmos.DrawLine(rightPosition, rightPosition_Up);

        float angle_both = Mathf.Atan2(LocalPosition_Ball.x, LocalPosition_Ball.z);
        Debug.Log("angle: " + (angle_both * Mathf.Rad2Deg));
        //Debug.Log("Position: " + LocalPosition_Ball.z);
        //Debug.Log("Magnitude: " + new Vector3(LocalPosition_Ball.x, 0, LocalPosition_Ball.z).magnitude);

        float distance_Ball = LocalPosition_Ball.x * LocalPosition_Ball.x + LocalPosition_Ball.z * LocalPosition_Ball.z;

        if (distance_Ball <= radius * radius && LocalPosition_Ball.y <= height && LocalPosition_Ball.y >= 0 && angle/2 >= Mathf.Abs(angle_both * Mathf.Rad2Deg))
        {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(LocalPosition_Ball, 0.5f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(LocalPosition_Ball, 0.5f);
        }
    }

    private void SphericalShape(Vector3 LocalPosition)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(default, radius);

        if(Vector3.Distance(LocalPosition, Vector3.zero) <= radius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(LocalPosition, 0.5f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(LocalPosition, 0.5f);
        }
    }

    private void ConeShape(Vector3 LocalPosition)
    {
        Vector3 rightWire = new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), 0, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        Vector3 leftWire = new Vector3(-radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), 0, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        Vector3 upWire = new Vector3(0, radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        Vector3 downWire = new Vector3(0, -radius * Mathf.Sin(Mathf.Deg2Rad * angle / 2), radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(default, rightWire);
        Gizmos.DrawLine(default, leftWire);
        Gizmos.DrawLine(default, upWire);
        Gizmos.DrawLine(default, downWire);

        Vector3 center = new Vector3(0, 0, radius * Mathf.Cos(Mathf.Deg2Rad * (angle / 2)));
        Vector3 normal = new Vector3(0, 0, radius);
        float radius_Cone = radius * Mathf.Sin(Mathf.Deg2Rad * (angle/2));

        Handles.color = Color.yellow; 
        Handles.DrawWireDisc(center, normal, radius_Cone);
        Handles.DrawWireArc(default, Vector3.up, leftWire, angle, radius);
        Handles.DrawWireArc(default, Vector3.right, upWire, angle, radius);

        float angleOnX = Mathf.Atan2(LocalPosition.x, LocalPosition.z);
        float angleOnY = Mathf.Atan2(LocalPosition.y, LocalPosition.z);

        if(Mathf.Abs(angleOnX * Mathf.Rad2Deg) <= angle/2 && Mathf.Abs(angleOnY * Mathf.Rad2Deg) <= angle/2 && Vector3.Distance(default, LocalPosition) <= radius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(LocalPosition, 0.5f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(LocalPosition, 0.5f);
        }

    }

    private enum Shapes
    {
        Wedge,
        Spherical,
        Cone
    }
}
