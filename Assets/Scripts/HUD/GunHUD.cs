using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;

    private GunScript gunScript;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        gunScript = gun.GetComponent<GunScript>();
    }

    void Update()
    {
        Debug.Log(gun);
        if (gun.activeSelf)
        {
            gameObject.SetActive(true);
            textMeshPro.text = gunScript.currentAmmo.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
