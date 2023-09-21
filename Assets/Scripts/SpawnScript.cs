using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnScript : MonoBehaviour
{
    private List<GameObject> obstacles;

    static private float secondToSpawn = 2f;

    private GameObject plane;

    private GameObject cloud;
    Vector3[] positions = {new Vector3(14, -1, 20), new Vector3(10, 0, 20), new Vector3(4, -11, 20), new Vector3(-14, -1, 20), new Vector3(-10, 0, 20), new Vector3(-4, -11, 20) };



    void Start()
    {
        obstacles = new List<GameObject>(Resources.LoadAll<GameObject>("Obstacles"));
        plane = Resources.Load("Plane", typeof(GameObject)) as GameObject;
        cloud = Resources.Load("DynamicSmoke", typeof(GameObject)) as GameObject;

        StartCoroutine(ObstacleSpawnMethod());
        StartCoroutine(SmokeSpawnMethod());
        StartCoroutine(PlaneSpawnMethod());
    }

    private int RandomObstacle()
    {
        return Random.Range(0, obstacles.Count - 1);
    }

    IEnumerator ObstacleSpawnMethod()
    {   
        while (GameManagerScript.gameOnState)
        {
            int obstacle = RandomObstacle();
            Instantiate(obstacles[obstacle], obstacles[obstacle].transform.position, obstacles[obstacle].transform.rotation);

            yield return new WaitForSeconds(secondToSpawn);
        }
    }

    private Vector3 RandomSmokePosition()
    {
        int randomIndex = Random.Range(0, positions.Length - 1);
        return positions[randomIndex];
    }

    private float RandomTimeToSpawnEnvironment()
    {
        float seconds = Random.Range(0.7f, 3f);
        return seconds;
    }

    IEnumerator SmokeSpawnMethod()
    {
        while(GameManagerScript.gameOnState)
        {
            Vector3 pos = RandomSmokePosition();
            Instantiate(cloud, pos, cloud.transform.rotation);

            yield return new WaitForSeconds(RandomTimeToSpawnEnvironment());
        }
    }

    IEnumerator PlaneSpawnMethod()
    {
        while(GameManagerScript.gameOnState)
        {
            Instantiate(plane, new Vector3(Random.Range(-1.5f, 1.5f), 0.001f, 20), plane.transform.rotation);

            yield return new WaitForSeconds(RandomTimeToSpawnEnvironment());
        }
    }
}
