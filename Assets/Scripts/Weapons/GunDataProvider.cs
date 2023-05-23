using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDataProvider : MonoBehaviour
{
    [HideInInspector]
    public int pickedUpAmmo = 0;
     [HideInInspector]
    public Material bulletMaterial;

    public void onBulletPickUp(Component sender, object data) {
        Debug.Log("onBulletPickUp event received with data: " + data);

        pickedUpAmmo += 1;

        if (data is Material) {
            Material material = (Material) data;
            bulletMaterial = material;
        }
    }
}