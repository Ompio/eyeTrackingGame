using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    private static UIControl _instance;
    public static UIControl Instance { get { return _instance; } }

    public static bool isPaused = false;
    public static bool congratulations = false;
    public GameObject pauseMenuUI;
    public GameObject endMenuUI;
    public GameObject debugUI;

    // Update is called once per frame
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        Resume();
        endMenuUI.SetActive(false);
        // Dodatkowe operacje inicjalizacyjne, jeśli są potrzebne
    }
    // void Start()
    // {
        
    // }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            debugUI.SetActive(false);
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        endMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EndScreen()
    {
        debugUI.SetActive(false);
        Debug.Log("koniec gry");
        endMenuUI.SetActive(true);
        Time.timeScale = 0.05f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.05f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
