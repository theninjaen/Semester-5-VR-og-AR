using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Transform[] controlPoints;

    public float routeSegments;

    private Vector3 gizmoPosition;
    [HideInInspector] public float lenght = 0;
    public Dictionary<float, Vector3> points = new Dictionary<float, Vector3>();

    //Gets called by controller, creates a library containing points along the route for the enemy to travel along
    public void MakeRoute()
    {
        float t = 0;

        for (int i = 0; i <= routeSegments ; i++)
        {
            // Formula for creating a bezier curve
            Vector3 routePoint = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            t += 1 / routeSegments;

            if (i == 0)
            {
                points.Add(0, routePoint);
                continue;
            }

            lenght += Vector3.Distance(routePoint, points[lenght]);

            points.Add(lenght, routePoint);
        }
    }

    // Debug tool, Draws route in editor
    private void OnDrawGizmos()
    {
        for (float t = 0; t<= 1; t += 0.05f)
        {
            gizmoPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmoPosition, 0.1f);
        }

        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
    }
}