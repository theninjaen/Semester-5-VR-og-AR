using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SerialController serialController;

    public GameObject enemy;
    public Transform straigthSpawn;
    public Transform straightTarget;

    public GameObject[] routeObjects;
    [HideInInspector] public Route[] routes;
    public bool goStraight = false;

    public bool spawnEnemies;
    private float timeSinceLastSpawn;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = spawnTime;
        routes = new Route[routeObjects.Length];

        for (int i = 0; i < routeObjects.Length; i++)
        {
            routes[i] = routeObjects[i].GetComponent<Route>();
            routes[i].MakeRoute();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    // Spawns new enemy basen on time since last spawn and spawn location
    private void SpawnEnemy()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (spawnEnemies && timeSinceLastSpawn >= spawnTime)
        {
            Vector3 spawnPosition = goStraight ? straigthSpawn.position : routes[0].points[0];
            Vector3 spawnRotationTarget = goStraight ? straightTarget.position : routes[0].points.ElementAt(1).Value;

            Vector3 spawnDirection = (spawnPosition - spawnRotationTarget).normalized;
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.forward, spawnDirection);

            EnemyAI newGuy = Instantiate(enemy, spawnPosition, spawnRotation).GetComponent<EnemyAI>();

            newGuy.routes = routes;
            newGuy.target = straightTarget;
            newGuy.goStraight = goStraight;
            newGuy.Ready();

            timeSinceLastSpawn = 0;
        }
    }

    //Changes the path of spawned enemies based on data recieved from the Arduino
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