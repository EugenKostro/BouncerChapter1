using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Scene1"); // zamijeni "Scene1" s pravim imenom tvoje prve scene
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit(); // izlaz iz igre
    }
    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu"); // Promijeni na ime scene glavnog izbornika
    }
}
