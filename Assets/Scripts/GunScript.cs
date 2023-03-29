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

    

    private Material bulletMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //input = transform.root.GetComponent<StarterAssetsInputs>();
         bulletMaterial = bulletPrefab.GetComponent<Renderer>().sharedMaterial;
        //bulletSpeed = GetComponent<float>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Mouse0))
       {
         Shoot();
       }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        bullet.GetComponent<Renderer>().sharedMaterial = bulletMaterial;
    }

    public void onBulletPickUp(Component sender, object data) {
        Debug.Log("onBulletPickUp event received with data: " + data);

        if (data is Material) {
            bulletMaterial = (Material) data;
        }
    }
}
