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

            GameObject enemyHolder = GameObject.FindGameObjectWithTag("EnemyHolder");
            bool allEnemiesDead = true;

            if (enemyHolder != null)
            {
                foreach (Transform child in enemyHolder.transform)
                {
                    if (child.CompareTag("Enemy"))
                    {
                        if (!child.GetComponent<RagdollScript>().isDead)
                        {
                            allEnemiesDead = false;
                            break;
                        }
                    }
                }
            }

            if (allEnemiesDead)
            {
                if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
                {
                    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                    SaveSystem.SaveProgress(nextSceneIndex);
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
