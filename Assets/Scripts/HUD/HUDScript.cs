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
            ammoTextMeshPro.text = gunScript.currentAmmo.ToString();
        }

        gunHUD.SetActive(gun.activeSelf);
        swordHUD.SetActive(sword.activeSelf);
        dashHUD.SetActive(dash.canDash);
    }

    public void onEnemyCountChanged(Component sender, object data) {
        Debug.Log("onEnemyCountChanged event received with data: " + data);

        if (data is int) {
            enemiesCounterText.text = ((int) data).ToString();
        }
    }
}
