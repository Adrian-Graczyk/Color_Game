using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnter : MonoBehaviour
{
    public CheckpointManager checkpointManager;

    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject virtualCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (checkpointManager.isLevelEndTriggerActivated)
            {
                virtualCamera.SetActive(true);
                timeline.SetActive(true);
            }
        }   
    }
}
