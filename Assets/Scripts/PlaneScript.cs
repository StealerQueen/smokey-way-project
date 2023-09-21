using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{   /// <summary>
    /// Speed parameter must be the same as Obstacle Speed and Cloud Speed.
    /// </summary>
    public float speed;

    void Update()
    {
        if (GameManagerScript.gameOnState)
        {
            transform.Translate(0, 0, -Math.Abs(speed) * ObstacleScript.speedCoefficient * Time.deltaTime);
        }

        if (gameObject.transform.position.z < -20) Destroy(gameObject);
    }
}
