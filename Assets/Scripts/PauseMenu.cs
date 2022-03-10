using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    GameObject _player;
    GameObject _enemy;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _enemy = GameObject.Find("Enemy Blueguard");
    }

    private void Update()
    {
        if (GameIsPaused == false)
        {
            Resume();
        }
        else
        {
            Pause();
        }

    }
    private void OnPause(InputValue pauseInput)
    {
        GameIsPaused = !GameIsPaused;
    }

    public void ButtonPause()
    {
        GameIsPaused = !GameIsPaused;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void SaveGame()
    {
        Debug.Log("Game Saved");
        GameController.instance.SaveGame();
    }

    public void LoadGame()
    {
        Resume();
        GameController.instance.LoadSaveGame();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}