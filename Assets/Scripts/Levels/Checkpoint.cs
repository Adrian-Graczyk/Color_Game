using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Checkpoint : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onNextCheckpointActivated;

    public List<GameObject> checkpointObjects;

    private List<GameObject> copiedObjects = new List<GameObject>();
    private BoxCollider collider;

    public void init()
    {
        copyObjects();

        collider = GetComponent<BoxCollider>();

        if (collider != null) {
            collider.enabled = false;
        }
    }

    public void enableCheckpointTrigger() 
    {
        if (collider != null) {
            collider.enabled = true;
        }
    }

    public bool areAllEnemiesDead() 
    {
        return copiedObjects
            .Where(obj => obj.CompareTag("Enemy"))
            .All(enemy => enemy.GetComponent<RagdollScript>().isDead);

    }

    public int getAliveEnemiesCount() 
    {
        return copiedObjects
            .Where(obj => obj.CompareTag("Enemy") && !obj.GetComponent<RagdollScript>().isDead)
            .ToList()
            .Count;
    }

    public void setActiveObjects(bool isActive) 
    {
        copiedObjects.ForEach(copy => { copy.SetActive(isActive); });
    }

    public void resetObjects()
    {
        Debug.Log("Objects to reset: " + copiedObjects.Count);

        copiedObjects.ForEach(copy => { Destroy(copy); });
        copiedObjects.Clear();

        copyObjects();
    }

    private void copyObjects()
    {
        Debug.Log("Objects to copy: " + checkpointObjects.Count);

        foreach (var checkpointObject in checkpointObjects)
        {
            GameObject copy = Instantiate(checkpointObject);
            copy.SetActive(true);
            checkpointObject.SetActive(false);

            Transform copyTransform = copy.GetComponent<Transform>();
            copyTransform.position = checkpointObject.GetComponent<Transform>().TransformPoint(Vector3.zero);

            UnityEngine.AI.NavMeshAgent copyAgent = copy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (copyAgent)
            {
                copyAgent.Warp(copyTransform.position);
            }
            
            copiedObjects.Add(copy);
        }
    }

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
