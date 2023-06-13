using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorsSurprise : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;
    [SerializeField] private GameObject enemy5;
    [SerializeField] private GameObject afterSurpiseTexts;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy1.SetActive(true);
            enemy2.SetActive(true);
            enemy3.SetActive(true);
            enemy4.SetActive(true);
            enemy5.SetActive(true);
            afterSurpiseTexts.SetActive(true);
        }
    }
}
