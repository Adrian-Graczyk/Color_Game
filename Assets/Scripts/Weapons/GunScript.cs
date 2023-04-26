using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunScript : MonoBehaviour
{
   
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bulletPoint;
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    int maxAmmo = 5;
    
    private int currentAmmo;
    private Material bulletMaterial;
    private Material gunMaterial;


    void Start()
    {
        //input = transform.root.GetComponent<StarterAssetsInputs>();
        bulletMaterial = bulletPrefab.GetComponent<Renderer>().sharedMaterial;
        gunMaterial = GetComponent<Renderer>().material;
        gunMaterial.SetColor("_EmissionColor", bulletMaterial.GetColor("_Color"));
        currentAmmo = maxAmmo;
        //bulletSpeed = GetComponent<float>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("CurrentAmmo: " + currentAmmo);

        if (currentAmmo > 0)
        {
            currentAmmo -= 1;
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            bullet.GetComponent<Renderer>().sharedMaterial = bulletMaterial;
        }
    }

    public void onBulletPickUp(Component sender, object data) {
        Debug.Log("onBulletPickUp event received with data: " + data);

        currentAmmo = Mathf.Min(currentAmmo + 1, maxAmmo);

        if (data is Material) {
            Material material = (Material) data;
            bulletMaterial = material;
            gunMaterial.SetColor("_EmissionColor", material.GetColor("_Color"));
        }
    }
}
