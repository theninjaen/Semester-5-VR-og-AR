using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    public Transform[] routes;
    private int nextRoute;
    private float tParameter;
    private Vector3 newPosition;
    public float speedModifier;
    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        nextRoute = 0;
        tParameter = 0;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FollowThisRoute(nextRoute));
        }
    }

    private IEnumerator FollowThisRoute(int nextRoute)
    {
        coroutineAllowed = false;
        Vector3 p0 = routes[nextRoute].GetChild(0).position;
        Vector3 p1 = routes[nextRoute].GetChild(1).position;
        Vector3 p2 = routes[nextRoute].GetChild(2).position;
        Vector3 p3 = routes[nextRoute].GetChild(3).position;

        while (tParameter < 1)
        {
            tParameter += Time.deltaTime * speedModifier;

            newPosition = Mathf.Pow(1 - tParameter, 3) * p0 +
                3 * Mathf.Pow(1 - tParameter, 2) * tParameter * p1 +
                3 * (1 - tParameter) * Mathf.Pow(tParameter, 2) * p2 +
                Mathf.Pow(tParameter, 3) * p3;

            transform.position = newPosition;
            yield return new WaitForEndOfFrame();
        }

        tParameter = 0;

        nextRoute++;

        if (nextRoute > routes.Length - 1)
        {
            nextRoute = 0;
        }

        coroutineAllowed = true;
    }
}
