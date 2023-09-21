using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    /// <summary>
    /// Speed parameter must be the same as Obstacle Speed.
    /// </summary>
    public float speed;
    static public float speedCoefficient = 1;

    void Update()
    {
        if (GameManagerScript.gameOnState)
        {
            transform.Translate(0, 0, -Math.Abs(speed) * speedCoefficient * Time.deltaTime);
        }

        if (gameObject.transform.position.z < -20) Destroy(gameObject);
    }
}
