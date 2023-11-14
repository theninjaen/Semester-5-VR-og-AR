using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public float speedModifier;
    public GameObject[] routesObjects;
    public Route[] routes;


    private Route route;
    private List<float> pointDistances;
    private float lastPointDistance;
    private float distanceTraveled;
    private int nextRoute;

    // Start is called before the first frame update
    void Start()
    {
        nextRoute = 0;
        lastPointDistance = 0;
        distanceTraveled = 0;

        routes = new Route[routesObjects.Length];

        for (int i = 0; i < routesObjects.Length; i++)
        {
            routes[i] = routesObjects[i].GetComponent<Route>();
            routes[i].MakeRoute();
        }

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

        MoveAlongRoute();
    }

    private void MoveAlongRoute()
    {
        distanceTraveled += Time.deltaTime * speedModifier;

        //If route is finished, sets up next route
        if (distanceTraveled > route.lenght)
        {
            nextRoute++;

            if (nextRoute > routes.Length - 1)
                nextRoute = 0;

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

            Debug.Log(route + " " + i);

            float lerpValue = (distanceTraveled - lastPointDistance) / (pointDistances[i] - lastPointDistance);

            transform.position = Vector3.Lerp(route.points[lastPointDistance], route.points[pointDistances[i]], lerpValue);

            break;
        }
    }
}
