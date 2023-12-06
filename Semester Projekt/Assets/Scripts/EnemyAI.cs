using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public float straightSpeed;
    public float routeSpeed;
    public float damage;
    public Route[] routes;
    public Transform target;
    [HideInInspector] public bool goStraight;
    public Scale scale;

    private Route route;
    private List<float> pointDistances;
    private float lastPointDistance;
    private float distanceTraveled;
    private int nextRoute;

    // Gets called by the controller after the enemy i spawned, after the it has set all relevant variables
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

    // Moves the enemy along the straight path
    private void MoveStraight()
    {
        if (Vector3.Distance(transform.position, target.position) < 0.1)
        {
            Destroy(gameObject); //HER SKA SØEN BLIVE GRØNNERE
            scale.increaseValue(damage);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, straightSpeed * Time.deltaTime);
    }

    // Moves the enemy along the curved route
    private void MoveAlongRoute()
    {
        distanceTraveled += Time.deltaTime * routeSpeed;

        // If route is finished, sets up next route, if last route finished, destroys this
        if (distanceTraveled > route.lenght)
        {
            nextRoute++;

            if (nextRoute > routes.Length - 1)
            {
                Destroy(gameObject); //HER SKAL SØEN BLIVER GRØNNERE
                scale.increaseValue(damage);
                return;
            }

            route = routes[nextRoute];
            pointDistances = new List<float>(route.points.Keys);

            distanceTraveled = 0;

            lastPointDistance = 0;
        }

        // Travel along route
        for (int i = pointDistances.IndexOf(lastPointDistance); i < pointDistances.Count; i++)
        {
            if (pointDistances[i] <= distanceTraveled)
            {
                lastPointDistance = pointDistances[i];
                continue;
            }

            float lerpValue = (distanceTraveled - lastPointDistance) / (pointDistances[i] - lastPointDistance);

            Vector3 targetPosition = Vector3.Lerp(route.points[lastPointDistance].position, route.points[pointDistances[i]].position, lerpValue);

            Vector3 targetDirection = Vector3.Lerp(route.points[lastPointDistance].tangent, route.points[pointDistances[i]].tangent, lerpValue).normalized;
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

            transform.position = targetPosition;
            transform.rotation = targetRotation;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, 0);

            break;
        }
    }
}
