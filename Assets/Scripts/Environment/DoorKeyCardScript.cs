using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyCardScript : MonoBehaviour
{

    [SerializeField] private Material openMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Keycard")
        {
            GetComponent<Renderer>().material = openMaterial;
            GameObject.Find("DoorKeyCard").GetComponent<Animator>().SetTrigger("character_nearby");
        }
    }

}
