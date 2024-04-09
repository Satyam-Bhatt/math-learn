using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveMesh : MonoBehaviour
{
    public Transform[] triangleVertices;
    public Transform outsidePoint;

    float WedgeProduct(Vector3 a, Vector3 b)
    {
        return (a.x * b.y - a.y * b.x) / Mathf.Abs(a.x * b.y - a.y * b.x);
    }

    float PointCheck(Vector3 p1, Vector3 p2, Vector3 pt)
    {
        Vector3 v1 = p2 - p1;
        Vector3 v2 = pt - p1;

        return WedgeProduct(v1, v2);
    }

    private void OnDrawGizmos()
    {
        int side1 = Mathf.RoundToInt(PointCheck(triangleVertices[0].position, triangleVertices[1].position, outsidePoint.position));
        int side2 = Mathf.RoundToInt(PointCheck(triangleVertices[1].position, triangleVertices[2].position, outsidePoint.position));
        int side3 = Mathf.RoundToInt(PointCheck(triangleVertices[2].position, triangleVertices[0].position, outsidePoint.position));

        Gizmos.color = (side1 == side2 && side2 == side3) ? Color.green : Color.red;

        Gizmos.DrawLine(triangleVertices[0].position, triangleVertices[1].position);
        Gizmos.DrawLine(triangleVertices[1].position, triangleVertices[2].position);
        Gizmos.DrawLine(triangleVertices[2].position, triangleVertices[0].position);
    }

}
