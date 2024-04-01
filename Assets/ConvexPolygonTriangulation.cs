using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexPolygonTriangulation : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();

    private Transform nextVertex = null;
    private List<Transform> vertexToBecomePoints = new List<Transform>();

    [ContextMenu ("Triangulate")]
    public void Triangulate()
    {
        Transform point = points[0];
        float distance_1 = 0;
        float distance_2 = 0;
        Transform vertex_1 = null;
        Transform vertex_2 = null;

        foreach (Transform vert_1 in points)
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
        foreach (Transform vert_2 in points)
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

        Debug.DrawLine(point.position, vertex_1.position, Color.red, 10f);
        Debug.DrawLine(point.position, vertex_2.position, Color.red, 10f);
        Debug.DrawLine(vertex_1.position, vertex_2.position, Color.red, 10f);
        Debug.Log("Drawn");

        points.Remove(point);
        vertexToBecomePoints.Add(vertex_1);
        vertexToBecomePoints.Add(vertex_2);
        
        while(points.Count > 2)
        {
            distance_1 = 0;
            point = vertexToBecomePoints[0];
            vertexToBecomePoints.RemoveAt(0);

            foreach (Transform vert_1 in points)
            {
                if(vert_1 != point && vert_1 != vertexToBecomePoints[0])
                {
                    float distance = Vector3.Distance(point.position, vert_1.position);
                    if(distance_1 == 0 || distance < distance_1)
                    {
                        distance_1 = distance;
                        vertex_1 = vert_1;
                    }
                }
            }

            Debug.DrawLine(point.position, vertex_1.position, Color.red, 10f);
            Debug.DrawLine(point.position, vertexToBecomePoints[0].position, Color.red, 10f);
            Debug.DrawLine(vertex_1.position, vertexToBecomePoints[0].position, Color.red, 10f);

            points.Remove(point);
            vertexToBecomePoints.Add(vertex_1);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
