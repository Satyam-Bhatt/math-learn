using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCalculator : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        int[] meshTriangles = GetComponent<MeshFilter>().sharedMesh.triangles;
    }

    [ContextMenu("Logger")]
    void Logger()
    {
        Mesh mesh;
        Vector3[] meshPoints = new Vector3[4];
        meshPoints[0] = new Vector3(0,1,0);
        meshPoints[1] = new Vector3(1,1,0);
        meshPoints[2] = new Vector3(1,0,0);
        meshPoints[3] = new Vector3(0,0,0);
    }
}
