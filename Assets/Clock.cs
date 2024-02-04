using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private float radius = 1;

    [SerializeField]
    private float hourMark = 1;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        Handles.color = Color.white;
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, radius);

        int hour = DateTime.Now.Hour;
        int minutes = DateTime.Now.Minute;
        int seconds = DateTime.Now.Second;
        
        Vector3 secondLocation = new Vector3(Mathf.Cos(-seconds * (6 * Mathf.Deg2Rad) + Mathf.PI / 2), Mathf.Sin(-seconds * (6 * Mathf.Deg2Rad) + Mathf.PI / 2), 0);
        Vector3 minuteLocation = new Vector3(Mathf.Cos(-minutes * (6 * Mathf.Deg2Rad) + Mathf.PI / 2), Mathf.Sin(-minutes * (6 * Mathf.Deg2Rad) + Mathf.PI / 2), 0);            
        Vector3 hourLocation = new Vector3(Mathf.Cos(- hour * (30 * Mathf.Deg2Rad) + Mathf.PI / 2), Mathf.Sin(- hour *(30 * Mathf.Deg2Rad) + Mathf.PI / 2), 0);

        Handles.color = Color.black;
        Handles.DrawLine(Vector3.zero, minuteLocation * 1.5f);

        Handles.color = Color.black;
        Handles.DrawLine(Vector3.zero, secondLocation);

        Handles.color = Color.red;
        Handles.DrawLine(Vector3.zero, hourLocation);

        int angle = 0;
        for (int i = 0; i <= 12; i++)
        {
            Vector3 hour_Mark = new Vector3(Mathf.Cos(-i * (30 * Mathf.Deg2Rad) + Mathf.PI / 2), Mathf.Sin(-i * (30 * Mathf.Deg2Rad) + Mathf.PI / 2), 0);
            Handles.color = Color.yellow;
            Handles.DrawLine(hour_Mark * 2, hour_Mark * radius);
            for (int j = 0; j <= 10; j++)
            {
                Vector3 minute_Mark = new Vector3(Mathf.Cos(-j * (3 * Mathf.Deg2Rad) + Mathf.PI / 2 + angle * Mathf.Deg2Rad), Mathf.Sin(-j * (3 * Mathf.Deg2Rad) + Mathf.PI / 2 + angle * Mathf.Deg2Rad), 0);
                Handles.color = Color.cyan;
                Handles.DrawLine(minute_Mark * 2.5f, minute_Mark * radius);
            }
            angle = angle + 30;
        }

    }
}
