using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private AudioSource gameAudio;

    void Start(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1.0f;

        openButton.onClick.AddListener(OpenImage);
        closeButton.onClick.AddListener(CloseImage);
    }
    void OpenImage(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    void CloseImage(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void NewGame(){
        SceneManager.LoadScene("PlatformerGame", LoadSceneMode.Single);
        Debug.Log("New Game Started");
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("Returning to Main Menu");
    }
    public void OpenSettings(){
        settingsMenu.SetActive(true);
        Debug.Log("Settings Menu Opened");
    }
    public void CloseSettings(){
        settingsMenu.SetActive(false);
        Debug.Log("Settings Menu Closed");
    }
    public void TurnSoundOn(){
        gameAudio.volume = 0.25f;
        Debug.Log("Sound is ON");
    }
    public void TurnSoundOff(){
        gameAudio.volume = 0;
        Debug.Log("Sound is OFF");
    }
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Game has been closed.");
    }
}