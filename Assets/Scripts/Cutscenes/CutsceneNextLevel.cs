using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneNextLevel : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene(1);
    }
}
