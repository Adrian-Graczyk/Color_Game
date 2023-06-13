using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelTrigger : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onPlayerDeath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerDeath.Raise(this, null);
        }
    }
}
