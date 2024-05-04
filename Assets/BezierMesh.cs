using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierMesh : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float t = 0;
    [SerializeField]private Transform[] points;

    Mesh mesh;

    private void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
               
        Vector3 tangent;

        Vector3 l1_ = Vector3.Lerp(points[0].localPosition, points[1].localPosition, t);
        Vector3 l2_ = Vector3.Lerp(points[1].localPosition, points[2].localPosition, t);
        Vector3 l3_ = Vector3.Lerp(points[2].localPosition, points[3].localPosition, t);

        Vector3 l12_ = Vector3.Lerp(l1_, l2_, t);
        Vector3 l23_ = Vector3.Lerp(l2_, l3_, t);

        Vector3 l123_ = Vector3.Lerp(l12_, l23_, t);

        tangent = (l23_ - l12_).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(l123_, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(l123_, tangent);
        Gizmos.DrawRay(l123_, new Vector3(-tangent.y,tangent.x,0)); //Up
        Gizmos.DrawRay(l123_, new Vector3(tangent.y, -tangent.x, 0)); //Down

        int detail = 32;
        Vector3 lastPosition = points[0].position;
        for (int i = 0; i <= detail; i++)
        {
            t = i / (float)detail;
            //Debug.Log(t);

            Vector3 l1 = Vector3.Lerp(points[0].localPosition, points[1].localPosition, t);
            Vector3 l2 = Vector3.Lerp(points[1].localPosition, points[2].localPosition, t);
            Vector3 l3 = Vector3.Lerp(points[2].localPosition, points[3].localPosition, t);

            Vector3 l12 = Vector3.Lerp(l1, l2, t);
            Vector3 l23 = Vector3.Lerp(l2, l3, t);

            Vector3 l123 = Vector3.Lerp(l12, l23, t);

            Handles.color = Color.magenta;
            Handles.DrawLine(lastPosition, l123, 10f);

            lastPosition = l123;
        }
    }

    [ContextMenu ("Make Plane")]
    public void MakePlane(Vector3 point1, Vector3 point2)
    {

        Vector3 p0 = point1 + 3 * Vector3.forward;
        Vector3 p1 = point1 - 3 * Vector3.forward;
        Vector3 p2 = point1 + 3 * Vector3.forward;
        Vector3 p3 = point1 - 3 * Vector3.forward;

        List<Vector3> vertices = new List<Vector3>()
        {
            p0, p1, p2, p3
        };

        List<int> triangles = new List<int>()
        {
            0,2,1, 1,2,3
        };

        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.up, Vector3.up, Vector3.up, Vector3.up
        };

        mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void Update()
    {
        int detail = 32;
        Vector3 lastPosition = points[0].position;

        List<Vector3> desiNormal = new List<Vector3>();

        List<Vector3> middlePoints = new List<Vector3>();
        for (int i = 0; i <= detail - 1; i++)
        {
            t = i / (float)detail;

            Vector3 l1 = Vector3.Lerp(points[0].position, points[1].position, t);
            Vector3 l2 = Vector3.Lerp(points[1].position, points[2].position, t);
            Vector3 l3 = Vector3.Lerp(points[2].position, points[3].position, t);

            Vector3 l12 = Vector3.Lerp(l1, l2, t);
            Vector3 l23 = Vector3.Lerp(l2, l3, t);

            Vector3 l123 = Vector3.Lerp(l12, l23, t);
            middlePoints.Add(l123);

            Vector3 tangent = (l23-l12).normalized;
            desiNormal.Add(new Vector3(-tangent.y, tangent.x, 0));
            desiNormal.Add(new Vector3(-tangent.y, tangent.x, 0));

            lastPosition = l123;
        }

        for (int i = 0; i <= detail - 1; i++)
        {
            t = i / (float)detail;

            Vector3 l1 = Vector3.Lerp(points[0].position, points[1].position, t);
            Vector3 l2 = Vector3.Lerp(points[1].position, points[2].position, t);
            Vector3 l3 = Vector3.Lerp(points[2].position, points[3].position, t);

            Vector3 l12 = Vector3.Lerp(l1, l2, t);
            Vector3 l23 = Vector3.Lerp(l2, l3, t);

            Vector3 l123 = Vector3.Lerp(l12, l23, t);
            middlePoints.Add(l123);

            Vector3 tangent = (l23 - l12).normalized;
            desiNormal.Add(new Vector3(tangent.y, -tangent.x, 0));
            desiNormal.Add(new Vector3(tangent.y, -tangent.x, 0));

            lastPosition = l123;
        }

        List<Vector3> vertices = new List<Vector3>();
        foreach (Vector3 point in middlePoints)
        {
            vertices.Add(point + 3 * Vector3.forward);
            vertices.Add(point - 3 * Vector3.forward);
        }

        Debug.Log("Verticies Count: "+vertices.Count + " Normal Count" + desiNormal.Count);

        List<int> triangles = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        int addfactor = 0;
        for(int i = 0; i <= vertices.Count/4 - 2; i = i + 1)
        {
            triangles.Add(0 + addfactor);
            triangles.Add(2 + addfactor);
            triangles.Add(1 + addfactor);

            triangles.Add(1 + addfactor);
            triangles.Add(2 + addfactor);
            triangles.Add(3 + addfactor);

            addfactor = addfactor + 2;
        }

        addfactor = 0;
        for (int i = 0; i <= vertices.Count / 4 - 2; i = i + 1)
        {
            triangles.Add(0 + addfactor);
            triangles.Add(1 + addfactor);
            triangles.Add(2 + addfactor);

            triangles.Add(1 + addfactor);
            triangles.Add(3 + addfactor);
            triangles.Add(2 + addfactor);

            addfactor = addfactor + 2;
        }

        Debug.Log("Triangles" + triangles.Count);

        for(int i = 0; i < triangles.Count; i = i + 3)
        {
            //Debug.Log(triangles[i] + " :1-> " +  triangles[i + 1] + " :2-> " + triangles[i + 2] + " :3-> ");
        }

        mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(desiNormal);

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
