using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [HideInInspector]
    public float score;
    [HideInInspector]
    static public float scoreCoefficient = 1f;
    [HideInInspector]
    static public bool gameOnState = true;

    private GameObject invisibleWalls;

    public TMP_Text scoreText;

    private AudioSource audioSource;

    private AudioClip mainMusic;

    private UnityEvent escapeButton = new UnityEvent();
    private UnityEvent resetButton = new UnityEvent();

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        invisibleWalls = GameObject.Find("Invisible walls");

        audioSource = GetComponent<AudioSource>();

        mainMusic = Resources.Load<AudioClip>("MainMusic");
        audioSource.clip = mainMusic;
        audioSource.Play();

        escapeButton.AddListener(ExitMethod);
        resetButton.AddListener(ResetMethod);
    }

    void Update()
    {
        escapeButton.Invoke(); // ???
        resetButton.Invoke();

        if (gameOnState)
        {
            score += scoreCoefficient * Time.deltaTime;
            scoreText.text = "Score: " + (Math.Round(score, 1) * 10);
        }

        WallMethod();
    }

    void ExitMethod()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }

    void ResetMethod()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameOnState = true;
            scoreCoefficient = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void WallMethod()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            counter++;
            if (counter % 2 == 0) invisibleWalls.SetActive(true);

            if (counter % 2 == 1) invisibleWalls.SetActive(false);
        }
    }
}
