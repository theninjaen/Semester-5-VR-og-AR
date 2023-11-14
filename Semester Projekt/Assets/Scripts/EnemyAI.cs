using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public float straightSpeed;
    public float routeSpeed;
    public Route[] routes;
    public Transform target;
    [HideInInspector] public bool goStraight;

    private Route route;
    private List<float> pointDistances;
    private float lastPointDistance;
    private float distanceTraveled;
    private int nextRoute;

    public void Ready()
    {
        if (goStraight)
            return;

        nextRoute = 0;
        lastPointDistance = 0;
        distanceTraveled = 0;

        route = routes[nextRoute];

        pointDistances = new List<float>(route.points.Keys);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (goStraight)
        {
            MoveStraight();
        }
        else
        {
            MoveAlongRoute();
        }
    }

    private void MoveStraight()
    {
        if (Vector3.Distance(transform.position, target.position) < 0.1)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, straightSpeed * Time.deltaTime);
    }

    private void MoveAlongRoute()
    {
        distanceTraveled += Time.deltaTime * routeSpeed;

        //If route is finished, sets up next route
        if (distanceTraveled > route.lenght)
        {
            nextRoute++;

            if (nextRoute > routes.Length - 1)
                Destroy(gameObject);

            route = routes[nextRoute];
            pointDistances = new List<float>(route.points.Keys);

            distanceTraveled = 0;

            lastPointDistance = 0;
        }

        //Travel along route
        for (int i = pointDistances.IndexOf(lastPointDistance); i < pointDistances.Count; i++)
        {
            if (pointDistances[i] <= distanceTraveled)
            {
                lastPointDistance = pointDistances[i];
                continue;
            }

            float lerpValue = (distanceTraveled - lastPointDistance) / (pointDistances[i] - lastPointDistance);

            transform.position = Vector3.Lerp(route.points[lastPointDistance], route.points[pointDistances[i]], lerpValue);

            break;
        }
    }
}
