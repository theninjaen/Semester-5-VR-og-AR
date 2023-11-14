using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SerialController serialController;

    public GameObject enemy;
    public Transform straigthSpawn;
    public Transform straightTarget;

    public GameObject[] routesObjects;
    [HideInInspector] public Route[] routes;
    public bool goStraight = false;

    public bool spawnEnemies;
    private float timeSinceLastSpawn = 0;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        routes = new Route[routesObjects.Length];

        for (int i = 0; i < routesObjects.Length; i++)
        {
            routes[i] = routesObjects[i].GetComponent<Route>();
            routes[i].MakeRoute();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (spawnEnemies && timeSinceLastSpawn >= spawnTime)
        {
            Vector3 spawnPosition = goStraight ? straigthSpawn.position : routes[0].controlPoints[0].position;

            EnemyAI newGuy = Instantiate(enemy, spawnPosition, Quaternion.identity).GetComponent<EnemyAI>();

            newGuy.routes = routes;
            newGuy.target = straightTarget;
            newGuy.goStraight = goStraight;
            newGuy.Ready();

            timeSinceLastSpawn = 0;
        }
    }

    void OnMessageArrived(string msg)
    {
        switch (msg)
        {
            case "h":
                goStraight = false;
                break;
            case "l":
                goStraight = true;
                break;
            default:
                Debug.LogWarning("Bad message: " +  msg);
                goStraight = false;
                break;
        }
    }
}