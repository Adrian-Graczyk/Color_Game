using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene_Bad : MonoBehaviour
{
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject timeline2;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeline.SetActive(false);
            timeline2.SetActive(true);
        }
    }
}
