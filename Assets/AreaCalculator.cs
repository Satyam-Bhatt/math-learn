using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaCalculator : MonoBehaviour
{
    Mesh mesh;
    private void OnDrawGizmos()
    {
       // int[] meshTriangles = GetComponent<MeshFilter>().sharedMesh.triangles;
    }

    [ContextMenu("Volume Calculate")]
    void VolumeCalculate()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;

        Vector3[] vertices = mesh.vertices;

        int[] triangles = mesh.triangles;
        float volume = 0;
        for(int i = 0; i < triangles.Length; i = i + 3)
        {
            Vector3 crossProduct = Vector3.Cross(vertices[triangles[0 + i]], vertices[triangles[1 + i]]);
            float dotProduct = Vector3.Dot(vertices[triangles[2 + i]], crossProduct);
            volume = volume + Mathf.Abs(dotProduct / 6);
            Debug.Log("Volume: " + volume);
        }
    }

    [ContextMenu("Area Calculate for Predefined Mesh")]
    void AreaCalculate_Predefined()
    {

        mesh = GetComponent<MeshFilter>().sharedMesh;

        Vector3[] vertices = mesh.vertices;

        int[] triangles = mesh.triangles;

       // Debug.Log("Vetices: " + mesh.vertices.Length + " Triangles: " + mesh.triangles.Length/3);

        float area = 0;
        for (int i = 0; i < triangles.Length; i = i + 3)
        {
            Vector3 vec_One = vertices[triangles[1 + i]] - vertices[triangles[0 + i]];
            Vector3 vec_Two = vertices[triangles[2 + i]] - vertices[triangles[0 + i]];

            float angle = Vector3.Angle(vec_One, vec_Two);
            Debug.Log("Angle: " + angle);
            float perperndicular_length = Vector3.Distance(vertices[triangles[1 + i]], vertices[triangles[0 + i]]) * Mathf.Sin(Mathf.Deg2Rad * angle);
            float base_length = Vector3.Distance(vertices[triangles[2 + i]], vertices[triangles[0 + i]]);
            area = area + 0.5f * base_length * perperndicular_length;
            Debug.Log("base: " + base_length + " Perpendicular: " + perperndicular_length + " Area: " + "" + area);
        }
        Debug.Log("Area: " + area);


    }


    [ContextMenu("Logger")]
    void Logger()
    {      
        if(mesh == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        List<Vector3> vertices = new List<Vector3>()
        {
            new Vector3(0, 0, 0),
            new Vector3(3, 3, 0),
            new Vector3(6, 0, 0),
            new Vector3(-5, 3, 0)
        };
        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };

        List<int> triangles = new List<int>() { 0, 1, 2, 0, 3, 1};

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);

        /*        Vector3 vec_One = vertices[triangles[1]] - vertices[triangles[0]];
        Vector3 vec_Two = vertices[triangles[2]] - vertices[triangles[0]];

        float angle = Vector3.Angle(vec_One, vec_Two);
        Debug.Log("Angle: " + angle);
        float perperndicular_length = Vector3.Distance(vertices[triangles[1]], vertices[triangles[0]]) * Mathf.Sin(Mathf.Deg2Rad * angle);
        float base_length = Vector3.Distance(vertices[triangles[2]], vertices[triangles[0]]);
        float area = 0.5f * base_length * perperndicular_length;
        Debug.Log("base: " + base_length + " Perpendicular: " + perperndicular_length + " Area: " + "" + area);*/

        for (int i = 0; i < triangles.Count; i = i + 3)
        {
            Vector3 vec_One = vertices[triangles[1 + i]] - vertices[triangles[0 + i]];
            Vector3 vec_Two = vertices[triangles[2 + i]] - vertices[triangles[0 + i]];

            float angle = Vector3.Angle(vec_One, vec_Two);
            Debug.Log("Angle: " + angle);
            float perperndicular_length = Vector3.Distance(vertices[triangles[1 + i]], vertices[triangles[0 + i]]) * Mathf.Sin(Mathf.Deg2Rad * angle);
            float base_length = Vector3.Distance(vertices[triangles[2 + i]], vertices[triangles[0 + i]]);
            float area = 0.5f * base_length * perperndicular_length;
            Debug.Log("base: " + base_length + " Perpendicular: " + perperndicular_length + " Area: " + "" + area);
        }


    }
}
