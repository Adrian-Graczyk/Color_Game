using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GunScript : MonoBehaviour
{
    StarterAssetsInputs input;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bulletPoint;
    [SerializeField]
    float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        input = transform.root.GetComponent<StarterAssetsInputs>();
        //bulletSpeed = GetComponent<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.shoot)
        {
            Shoot();
            input.shoot = false;
        }
    }

    void Shoot()
    {
        //Debug.Log("SHOOT");
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }
}
