using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosCircle : MonoBehaviour
{
    public float radius = 1f;       // Circle radius
    public int segments = 36;       // How smooth the circle is
    public Color color = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Vector3 previousPoint = transform.position + Vector3.right * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Gizmos.DrawLine(previousPoint, newPoint);
            previousPoint = newPoint;
        }
    }
}
