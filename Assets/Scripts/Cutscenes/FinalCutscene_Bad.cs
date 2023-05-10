using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene_Bad : MonoBehaviour
{
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject timeline2;
    [SerializeField] private GameObject virtualCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
                //virtualCamera.SetActive(true);
                timeline.SetActive(false);
                timeline2.SetActive(true);
        }
    }
}
