using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
    public void NewGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene("PlatformerGame", LoadSceneMode.Single);
    }

    public void MainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Suljettu");
    }
}