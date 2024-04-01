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
        if(nextVertex == null)
        {
            nextVertex = points[0];
        }
        Transform point = nextVertex;
        float distance_1 = 0;
        float distance_2 = 0;
        Transform vertex_1 = null;
        Transform vertex_2 = null;

        foreach (Transform point2 in points)
        {
            if (point != point2)
            {
                float distance = Vector3.Distance(point.position, point2.position);
                if (distance_1 == 0 || distance < distance_1)
                {
                    distance_1 = distance;
                    vertex_1 = point2;
                }
            }
        }
        foreach (Transform point3 in points)
        {
            if (point != point3 && point3 != vertex_1)
            {
                float distance = Vector3.Distance(point.position, point3.position);
                if (distance_2 == 0 || distance < distance_2)
                {
                    distance_2 = distance;
                    vertex_2 = point3;
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

        if(points.Count > 2)
        {
            nextVertex = vertexToBecomePoints[0];
            vertexToBecomePoints.RemoveAt(0);
            //Triangulate();
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
