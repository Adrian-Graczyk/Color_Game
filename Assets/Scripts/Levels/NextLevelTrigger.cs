using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
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
                if(ob.CompareTag("Enemy"))
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
                if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else
                    SceneManager.LoadScene(0);
            }
        }
    }
}
