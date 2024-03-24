using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [Range(0,100)]
    public float health = 100;



    private void OnDrawGizmos()
    {
        float greenColor = Mathf.InverseLerp(0, 100, health);
        float redColor = Mathf.InverseLerp(100, 0, health);
        
        Color color = new Color(redColor, greenColor, 0, 1);
        Handles.color = color;

        Handles.DrawLine(transform.position, transform.position + Vector3.right * health, 10f);

        Vector3[] points = new Vector3[121];
        int index = 0;
        for(int i = 0; i <= 360; i = i + 3)
        {
            points[index] = transform.position + 5 * (AngleToDirection(i) + new Vector3(0,0,index * 0.01f));
            index++;
        }


        Handles.color = color;
        Handles.DrawAAPolyLine(4f, points);
    }

    Vector3 AngleToDirection(float angle)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
