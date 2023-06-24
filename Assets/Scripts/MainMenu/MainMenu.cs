using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void quitGame() {
        Debug.Log("QUIT game!");
        Application.Quit();
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(6);
    }
}
