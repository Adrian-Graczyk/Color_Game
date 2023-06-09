using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onNextCheckpointActivated;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Next checkpoint activated");
            GetComponent<BoxCollider>().enabled = false;
            onNextCheckpointActivated.Raise(this, null);
        }
    }
}
