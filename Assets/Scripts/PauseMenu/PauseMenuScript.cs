using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private MoveCamera moveCameraScript;
    [SerializeField] private PlayerLook playerLookScript;
    [SerializeField] private AudioListener audioListener; 
    [SerializeField] private GameObject fpsCamera;

    private GameObject pausePanel;
    private bool isPaused;

    private CursorLockMode desiredMode;

    private void Start()
    {
        isPaused = false;
        pausePanel = transform.GetChild(0).gameObject;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.Escape)) { return;}
             
        if(!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
       
        DisablePlayer();
       
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        EnablePlayer();
        
    }

    public void BackToMenu()
    {
        Resume();
        EnablePlayer();
        SceneManager.LoadScene(1);
    }


    void DisablePlayer()
    {
        moveCameraScript.enabled = false;
        playerLookScript.enabled = false;
        audioListener.enabled = false;

        Cursor.visible = true;
        desiredMode = CursorLockMode.None;
        {
            Cursor.lockState = desiredMode;
        }


        fpsCamera.SetActive(false);
    }

    void EnablePlayer()
    {
        moveCameraScript.enabled = true;
        playerLookScript.enabled = true;
        audioListener.enabled = true;

        fpsCamera.SetActive(true);
        desiredMode = CursorLockMode.Confined;
        Cursor.visible = false;
    }

}
