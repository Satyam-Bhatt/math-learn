using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Placer : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform detectGameObject;
    [SerializeField] private Transform rotatorOfTurret;
    [SerializeField] private Transform turretBase;

    [SerializeField] private float radius = 5f;
    [SerializeField] private float height = 2f;

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, hit.point);

            Vector3 yAxis = hit.normal;
            Vector3 xAxis = transform.right;
            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(hit.point, yAxis);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(hit.point, xAxis);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(hit.point, zAxis);
        }
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 yAxis = hit.normal;
            Vector3 xAxis = transform.right;
            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

            turret.transform.position = hit.point;
            turret.transform.rotation = Quaternion.LookRotation(zAxis, yAxis);

            if (Input.mouseScrollDelta.y > -2)
            {
                turretBase.rotation = turretBase.rotation * Quaternion.Euler(0, Input.mouseScrollDelta.y * 10, 0);
            }

        }


        var c = Camera.main.transform;
        c.Rotate(0, Input.GetAxis("Mouse X"), 0);
        c.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        bool DistanceCheck(Vector3 t)
        {
            float xDist = t.x;
            float yDist = t.y;
            float zDist = t.z;

            if (Mathf.Abs(xDist) <= radius && yDist <= height && yDist >= 0 && Mathf.Abs(zDist) <= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        float DotProduct(Vector3 zAxis, Vector3 pontInSpace)
        {
            return Vector3.Dot(turret.transform.InverseTransformPoint(zAxis), pontInSpace);
        }

        void RotateGun(Transform t, Vector3 lookAt)
        {
            Vector3 lookAtDirection = (lookAt - t.position).normalized;
            Quaternion targetRoation = Quaternion.LookRotation(lookAtDirection);
            t.rotation = Quaternion.Slerp(t.rotation, targetRoation, 10 * Time.deltaTime);
        }

        void HitAndBounce(Transform t, Vector3 target)
        {
            Vector3 shootDirection = (target - t.position).normalized;
            Ray ray = new Ray(t.position, shootDirection);
            RaycastHit hit;

            Gizmos.color = Color.black;
            Gizmos.DrawRay(t.position, shootDirection);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 yDir = ray.direction - 2 * hit.normal * Vector3.Dot(hit.normal, ray.direction);

                Gizmos.color = Color.green;
                Gizmos.DrawRay(hit.point, -shootDirection);

                Gizmos.color = Color.red;
                Gizmos.DrawRay(hit.point, yDir.normalized);
            }
        }
    }
}
