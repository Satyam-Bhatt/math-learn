using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveMesh : MonoBehaviour
{
    public List<Transform> triangleVertices = new List<Transform>();

    private List<Transform> vertextToBecomePoints = new List<Transform>();
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

/*    private void OnDrawGizmos()
    {
        int side1 = Mathf.RoundToInt(PointCheck(triangleVertices[0].position, triangleVertices[1].position, outsidePoint.position));
        int side2 = Mathf.RoundToInt(PointCheck(triangleVertices[1].position, triangleVertices[2].position, outsidePoint.position));
        int side3 = Mathf.RoundToInt(PointCheck(triangleVertices[2].position, triangleVertices[0].position, outsidePoint.position));

        Gizmos.color = (side1 == side2 && side2 == side3) ? Color.green : Color.red;

        Gizmos.DrawLine(triangleVertices[0].position, triangleVertices[1].position);
        Gizmos.DrawLine(triangleVertices[1].position, triangleVertices[2].position);
        Gizmos.DrawLine(triangleVertices[2].position, triangleVertices[0].position);
    }*/

    private bool PointInsideOrOutside(Transform v1, Transform v2, Transform v3, Transform pt)
    {
        int side1 = Mathf.RoundToInt(PointCheck(v1.position, v2.position, pt.position));
        int side2 = Mathf.RoundToInt(PointCheck(v2.position, v3.position, pt.position));
        int side3 = Mathf.RoundToInt(PointCheck(v3.position, v1.position, pt.position));

        if(side1 == side2 && side2 == side3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [ContextMenu ("Concave Mehs")]
    public void Concave()
    {
        Transform point = triangleVertices[0];
        float distance_1 = 0;
        float distance_2 = 0;
        Transform vertex_1 = null;
        Transform vertex_2 = null;

        foreach (Transform vert_1 in triangleVertices)
        {
            if(vert_1 != point)
            {
                float distance = Vector3.Distance(point.position, vert_1.position);
                if(distance_1 == 0 || distance < distance_1)
                {
                    distance_1 = distance;
                    vertex_1 = vert_1;
                }
            }
        }
        foreach (Transform vert_2 in triangleVertices)
        {
            if(vert_2 != point && vert_2 != vertex_1)
            {
                float distance = Vector3.Distance(point.position, vert_2.position);
                if(distance_2 == 0 || distance < distance_2)
                {
                    distance_2 = distance;
                    vertex_2 = vert_2;
                }
            }
        }

        foreach(Transform point_ in triangleVertices)
        {
            if(point_ != point && point_ != vertex_1 && point_ != vertex_2)
            {
                if(PointInsideOrOutside(vertex_1, vertex_2, point, point_))
                {
                    vertex_2 = point_;
                }
            }
        }

        Debug.DrawLine(point.position, vertex_1.position, Color.cyan , 20f);
        Debug.DrawLine(point.position, vertex_2.position, Color.cyan, 20f);
        Debug.DrawLine(vertex_1.position, vertex_2.position, Color.cyan, 20f);

        triangleVertices.Remove(point);
        vertextToBecomePoints.Add(vertex_1);
        vertextToBecomePoints.Add(vertex_2);

        while(triangleVertices.Count > 2)
        {
            distance_1 = 0;
            point = vertextToBecomePoints[0];
            vertextToBecomePoints.RemoveAt(0);

            foreach (Transform vert_1 in triangleVertices)
            {
                if(vert_1 != point && vert_1 != vertextToBecomePoints[0])
                {
                    float distance = Vector3.Distance(point.position, vert_1.position);
                    if(distance_1 == 0 || distance < distance_1)
                    {
                        distance_1 = distance;
                        vertex_1 = vert_1;
                    }
                }
            }

            foreach (Transform vert_2 in triangleVertices)
            {
                if(vert_2 != point && vert_2 != vertex_1 && vert_2 != vertextToBecomePoints[0])
                {
                    if (PointInsideOrOutside(vertex_1, point, vertextToBecomePoints[0], vert_2))
                    {
                        vertex_1 = vert_2;
                    }
                }
            }

            Debug.DrawLine(point.position, vertex_1.position, Color.cyan, 20f);
            Debug.DrawLine(point.position, vertextToBecomePoints[0].position, Color.cyan, 20f);
            Debug.DrawLine(vertex_1.position, vertextToBecomePoints[0].position, Color.cyan, 20f);

            triangleVertices.Remove(point);
            vertextToBecomePoints.Add(vertex_1);
        }

    }

}
