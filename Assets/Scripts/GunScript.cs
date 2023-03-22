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

    // Start is called before the first frame update
    void Start()
    {
        //input = transform.root.GetComponent<StarterAssetsInputs>();
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
        Debug.Log("SHOOT");
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }
}
