using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private MoveCamera moveCameraScript;
    private bool isPaused;

    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (!Input.GetKey(KeyCode.Escape)) { return;}
             
        if(!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            moveCameraScript.enabled = false;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            moveCameraScript.enabled = true;
        }
    }
}
