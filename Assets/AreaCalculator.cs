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
            new Vector3(4, 5, 0),
            new Vector3(6, 0, 0),
           // new Vector3(0, 3, 0)
        };
        List<Vector3> normals = new List<Vector3>()
        {
            //Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };

        List<int> triangles = new List<int>() { 0, 1, 2 };//, 0, 1, 3};

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);

/*        Vector3 vec_One = vertices[1] - vertices[0];
        Vector3 vec_Two = vertices[2] - vertices[0];

        float angle = Vector3.Angle(vec_One, vec_Two);
        Debug.Log("Angle: " + angle);
        float perperndicular_length = Vector3.Distance(vertices[1], vertices[0]) * Mathf.Sin(Mathf.Deg2Rad * angle);
        float base_length = Vector3.Distance(vertices[2], vertices[0]);
        float area = 0.5f * base_length * perperndicular_length;
        Debug.Log("base: " + base_length + " Perpendicular: " + perperndicular_length + " Area: "+""+area);*/
    }
}
