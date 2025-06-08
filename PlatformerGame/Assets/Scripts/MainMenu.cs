using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    public AudioSource gameAudio;

    void Start(){
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
    public void PlayGame(){
        SceneManager.LoadScene("PlatformerGame", LoadSceneMode.Single);
    }
    public void SettingsOpen(){
        settingsMenu.SetActive(true);
    }
    public void SettingsClose(){
        settingsMenu.SetActive(false);
    }
    public void CreditsOpen(){
        creditsMenu.SetActive(true);
    }
    public void CreditsClose(){
        creditsMenu.SetActive(false);
    }
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Game has been closed.");
    }
    public void TurnSoundOn(){
        gameAudio.volume = 1;
        Debug.Log("Sound is ON");
    }
    public void TurnSoundOff(){
        gameAudio.volume = 0;
        Debug.Log("Sound is OFF");
    }
}