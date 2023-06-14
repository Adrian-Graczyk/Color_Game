using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Player : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject fpsCamera;
    [SerializeField] private WeaponSwitcher weaponSwitcher;


    private Material gunMaterial;
    private void Start()
    {
        gunMaterial = gun.GetComponent<Renderer>().material;


        gunMaterial.SetColor("_EmissionColor", new Color(0, 255, 255));
        GameObject.FindGameObjectWithTag("Player").SetActive(false);


        weaponSwitcher.enabled = false;


        foreach (Transform weapon in fpsCamera.transform)
        {
            if (weapon.gameObject.CompareTag("Gun"))
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
        }
    }
}
