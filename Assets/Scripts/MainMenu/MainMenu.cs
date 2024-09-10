using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Scene1"); 
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit(); 
    }
    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu"); 
    }
}
