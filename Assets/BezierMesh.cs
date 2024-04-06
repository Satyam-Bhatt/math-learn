using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMesh : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float t = 0;
    private Transform[] points;

    private void OnDrawGizmos()
    {
        
    }
}
