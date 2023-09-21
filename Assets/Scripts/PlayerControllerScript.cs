using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody playerRB;

    [SerializeField] private float jumpForce;
    [SerializeField] private float changePositionForce;
    static public float changePositionForceCoefficient = 1;
    [SerializeField] private int jumps;

    private AudioSource audioSource;

    private AudioClip jumpSound;
    private AudioClip hitSound;

    private int jumpsCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        jumpSound = Resources.Load<AudioClip>("jump");
        hitSound = Resources.Load<AudioClip>("hit");
    }

    private void Update()
    {
        if (GameManagerScript.gameOnState)
        {
            ControlMethod();
            JumpsCounterMethod(jumps);
        }

        if (transform.position.y < -1.5f)
        {
            GameManagerScript.gameOnState = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plate")) jumpsCounter = 0;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManagerScript.gameOnState = false;
            GameManagerScript.scoreCoefficient = 0;

            audioSource.PlayOneShot(hitSound);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManagerScript.scoreCoefficient += 0.1f;
        ObstacleScript.speedCoefficient += 0.05f;
        changePositionForceCoefficient += 0.025f;
    }

    private void JumpsCounterMethod(int _jumps)
    {
        if (jumpsCounter < _jumps && Input.GetKeyDown(KeyCode.Space))
        {
            jumpsCounter++;
            playerRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void ControlMethod()
    {
        if (Input.GetKey(KeyCode.E)) playerRB.velocity += new Vector3(-changePositionForce * changePositionForceCoefficient * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.R)) playerRB.velocity += new Vector3(changePositionForce * changePositionForceCoefficient * Time.deltaTime, 0, 0);
    }


}
