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

    public GunDataProvider gunDataProvider;
    
    private int currentAmmo;
    private Material bulletMaterial;
    private Material gunMaterial;

    [SerializeField] private AudioSource playerShotSound;

    void Start()
    {
        bulletMaterial = bulletPrefab.GetComponent<Renderer>().sharedMaterial;
        gunMaterial = GetComponent<Renderer>().material;
        gunMaterial.SetColor("_EmissionColor", bulletMaterial.GetColor("_Color"));
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        UpdateGunData();
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

            playerShotSound.Play();
        }
    }

    void UpdateGunData()
    {
        currentAmmo = Mathf.Min(maxAmmo, currentAmmo + gunDataProvider.pickedUpAmmo);
        gunDataProvider.pickedUpAmmo = 0;

        if (gunDataProvider.bulletMaterial != null)
        {
            gunMaterial.SetColor("_EmissionColor", gunDataProvider.bulletMaterial.GetColor("_Color"));
            bulletMaterial = gunDataProvider.bulletMaterial;
        }
    }
}
