using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;

    [Header("PlayerWeaponsData")]
    public GunDataProvider gunDataProvider;
    public GunScript gunScript;
    public BladeColorChange bladeColorScript;

    private int currentCheckpoint = 0;
    private WeaponsCheckpointData weaponsData = new WeaponsCheckpointData();

    void Start()
    {
        checkpoints.ForEach(checkpoint => checkpoint.init());

        readPlayerWeaponsData();
        activateCheckpoints();
    }

    void Update()
    {
        if (checkpoints[currentCheckpoint].areAllEnemiesDead() && (currentCheckpoint + 1 < checkpoints.Count)) {
            checkpoints[currentCheckpoint + 1].enableCheckpointTrigger();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            checkpoints[currentCheckpoint].resetObjects();
            resetPlayer();
            clearBullets();
        }
    }

    public void onNextCheckpointActivated(Component sender, object data)
    {
        Debug.Log("onNextCheckpointActivated event received with data: " + data);

        currentCheckpoint += 1;

        if (currentCheckpoint >= checkpoints.Count) {
            Debug.LogError("Current checkpoint: " + currentCheckpoint + " is greater than checkpoints count: " + checkpoints.Count);
            return;
        }

        if (currentCheckpoint == checkpoints.Count - 1) {
            completeLevel();
            return;
        }

        readPlayerWeaponsData();
        activateCheckpoints();
    }

    private void activateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].setActiveObjects(i <= currentCheckpoint);
        }
    }

    private void resetPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        player.transform.rotation = checkpoints[currentCheckpoint].transform.rotation;

        resetPlayerWeapons();
    }

    private void readPlayerWeaponsData()
    {
        (weaponsData.gunAmmo, weaponsData.gunBulletMaterial) = gunScript.getCurrentGunData();
        weaponsData.swordMaterial = bladeColorScript.getBladeMaterial();

        Debug.Log("PlayerWeaponsData read. gunAmmo: " + weaponsData.gunAmmo + " gunBulletMaterial: " + 
                   weaponsData.gunBulletMaterial + " swordMaterial: " + weaponsData.swordMaterial);
    }

    private void resetPlayerWeapons()
    {
        gunDataProvider.pickedUpAmmo = 0;
        gunDataProvider.bulletMaterial = weaponsData.gunBulletMaterial;
        gunScript.setCurrentAmmo(weaponsData.gunAmmo);
        bladeColorScript.setBladeMaterial(weaponsData.swordMaterial);
    }

    private void clearBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (var bullet in bullets) {
            Destroy(bullet);
        }
    }

    private void completeLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        Debug.Log("Level completed, nextSceneIndex: " + nextSceneIndex + " sceneCountInBuildSettings: " + SceneManager.sceneCountInBuildSettings);

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            if (SaveSystem.LoadProgress().currentSceneIndex < nextSceneIndex) {
                SaveSystem.SaveProgress(nextSceneIndex);
            }

            SceneManager.LoadScene(0);  // go back to MainMenu
        }
    }
}
