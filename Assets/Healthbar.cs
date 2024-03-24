using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [Range(0,100)]
    public float health = 100;

    [Header("Spring Question")]

    [Range(0,100)]
    public int turns = 2;
    public float radius = 2;
    public float height = 0;

    private void OnDrawGizmos()
    {
        float greenColor = Mathf.InverseLerp(0, 100, health);
        float redColor = Mathf.InverseLerp(100, 0, health);
        
        Color color = new Color(redColor, greenColor, 0, 1);
        Handles.color = color;

        Handles.DrawLine(transform.position, transform.position + Vector3.right * health, 10f);

        Vector3[] points = new Vector3[(turns * 360)/3 + 1];
        int index = 0;
        for(int i = 0; i <= turns * 360; i = i + 3)
        {
            float num1 = i / 3;
            float num2 = (turns * 360) / 3;
            float divided = num1 / num2;
            points[index] = transform.position + radius * (AngleToDirection(i) + new Vector3(0,0,divided * height));
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
