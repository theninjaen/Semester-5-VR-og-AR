using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;

    public Transform[] routes;
    private Route route;
    private List<float> pointDistances;
    private float lastPointDistance;
    private int nextRoute;
    public float speedModifier;
    private float distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        nextRoute = 0;
        lastPointDistance = 0;
        distanceTraveled = 0;

        route = routes[nextRoute].GetComponent<Route>();
        pointDistances = new List<float>(route.points.Keys);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongRoute();
    }

    private void MoveAlongRoute()
    {
        distanceTraveled += Time.deltaTime * speedModifier;

        if (distanceTraveled > route.lenght)
        {
            nextRoute++;

            if (nextRoute > routes.Length - 1)
                nextRoute = 0;

            route = routes[nextRoute].GetComponent<Route>();
            pointDistances = new List<float>(route.points.Keys);

            distanceTraveled = 0;

            lastPointDistance = 0;
        }

        for (int i = pointDistances.IndexOf(lastPointDistance); i < pointDistances.Count; i++)
        {
            if (pointDistances[i] < distanceTraveled)
            {
                lastPointDistance = pointDistances[i];
                continue;
            }

            if (pointDistances[i] - distanceTraveled < distanceTraveled - lastPointDistance)
            {
                transform.position = route.points[pointDistances[i]];
                lastPointDistance = pointDistances[i];
            }
            else
            {
                transform.position = route.points[lastPointDistance];
            }

            break;
        }
    }
}
