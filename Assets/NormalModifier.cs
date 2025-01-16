using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalModifier : MonoBehaviour
{
    MeshFilter meshFilter;
    Mesh mesh;

    public Vector3 normalDir;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = meshFilter.mesh;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] normalSet = new Vector3[mesh.normals.Length];
        for (int i = 0; i < mesh.normals.Length; i++)
        {
            //Debug.Log(mesh.normals[i]);
            normalSet[i] = normalDir.normalized;
        }

        mesh.SetNormals(normalSet);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        mesh = GetComponent<MeshFilter>().sharedMesh;

        Vector3[] normalSet = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            Vector3 v = Vector3.zero;
            if (i == 0)
            {
                Gizmos.color = Color.red;
                v = new Vector3(0, 0, -1);
            }
            else if (i == 1)
            {
                Gizmos.color = Color.green;
                v = new Vector3(0, 0, -1);

            }
            else if (i == 2)
            {
                Gizmos.color = Color.blue;
                v = new Vector3(0, 1, 0);

            }
            else
            {
                Gizmos.color = Color.black;
                v = new Vector3(0, 1, 0);

            }
            Gizmos.DrawSphere(mesh.vertices[i], 0.1f);
            Gizmos.DrawLine(mesh.vertices[i], mesh.vertices[i] + v);

            //Debug.Log(mesh.normals[i]);
            normalSet[i] = v;
        }

        mesh.SetNormals(normalSet);
    }
}
