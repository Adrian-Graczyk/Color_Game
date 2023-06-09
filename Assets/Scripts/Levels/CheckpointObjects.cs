using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObjects : MonoBehaviour
{
    public List<GameObject> checkpointObjects;

    private List<GameObject> copiedObjects = new List<GameObject>();

    void Start()
    {
        copyObjects();
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
}
