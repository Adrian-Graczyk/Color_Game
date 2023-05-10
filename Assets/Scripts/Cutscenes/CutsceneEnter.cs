using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnter : MonoBehaviour
{
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject virtualCamera;
    private void OnTriggerEnter(Collider other)
    {
        int enemiesAlive = 0;
        if (other.CompareTag("Player"))
        {
            Debug.Log("NextLevelTrigger");
            Scene thisScene = SceneManager.GetActiveScene();

            GameObject[] objectsOnScene = thisScene.GetRootGameObjects();
            bool allEnemiesDead = true;


            foreach (GameObject ob in objectsOnScene)
            {
                if (ob.CompareTag("Enemy"))
                {
                    if (!ob.GetComponent<RagdollScript>().isDead)
                    {
                        allEnemiesDead = false;
                        break;
                    }
                }

            }

            if (allEnemiesDead)
            {
                virtualCamera.SetActive(true);
                timeline.SetActive(true);
            }
        }
    }
}
