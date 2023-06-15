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
    private bool canESC;

    private CursorLockMode desiredMode;

    private void Start()
    {
        isPaused = false;
        pausePanel = transform.GetChild(0).gameObject;
        pausePanel.SetActive(false);
        canESC = true;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || !canESC) { return;}
             
        if(!isPaused)
        {
            Pause();
            StartCoroutine(Wait());
        }
        else
        {
            Resume();
            StartCoroutine(Wait());
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

    IEnumerator Wait()
    {
        canESC = false;       
        yield return new WaitForSecondsRealtime(0.2f);
        canESC = true;
    }
}
