using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCutscene : MonoBehaviour
{
    [SerializeField ]private GameObject cutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cutscene.SetActive(true);
        }
    }
}
