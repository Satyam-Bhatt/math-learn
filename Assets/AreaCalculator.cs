using System.Collections;
using System.Collections.Generic;
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
            new Vector3(0,1,0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 0, 0)
        };
        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };

        List<int> triangles = new List<int>() { 3, 1, 2, 3, 0, 1 };

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);
    }
}
