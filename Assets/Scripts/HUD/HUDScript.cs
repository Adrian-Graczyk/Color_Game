using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private Dash dash;


    private GameObject gunHUD;
    private GameObject swordHUD;
    private GameObject dashHUD;

    private TextMeshProUGUI enemiesCounterText;
    private TextMeshProUGUI ammoTextMeshPro;

    private GunScript gunScript;

    private Scene thisScene;
    private GameObject enemyHolder;



    void Start()
    {
        gunHUD = transform.Find("Gun").gameObject;
        swordHUD = transform.Find("Sword").gameObject;
        dashHUD = transform.Find("Dash").gameObject;

        enemiesCounterText = transform.Find("Enemies").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        ammoTextMeshPro = gunHUD.GetComponentInChildren<TextMeshProUGUI>();

        gunScript = gun.GetComponent<GunScript>();

        thisScene = SceneManager.GetActiveScene();
        enemyHolder = GameObject.FindGameObjectWithTag("EnemyHolder");
    }

    void Update()
    {
        if (gun.activeSelf)
        {
            gunHUD.SetActive(true);
            ammoTextMeshPro.text = gunScript.currentAmmo.ToString();
        }
        else
        {
            gunHUD.SetActive(false);
        }


        if (sword.activeSelf)
            swordHUD.SetActive(true);
        else
            swordHUD.SetActive(false);

        if (dash.canDash)
            dashHUD.SetActive(true);
        else
            dashHUD.SetActive(false);

        SetEnemiesCounter();

    }

    void SetEnemiesCounter()
    {
        enemiesCounterText.text = GetEnemiesAlive().ToString();

    }
        
    int GetEnemiesAlive()
    {
        int aliveEnemiesCounter = 0;
        if (enemyHolder != null)
        {
            foreach (Transform child in enemyHolder.transform)
            {
                if (child.CompareTag("Enemy"))
                {
                    if (!child.GetComponent<RagdollScript>().isDead)
                    {
                        aliveEnemiesCounter++;
                    }
                }
            }
        }
        return aliveEnemiesCounter;
    }
}
