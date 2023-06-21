using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerFinder
{
    public static GameObject playerGameObject()
    {
        GameObject[] playerTagObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = null;

        foreach (GameObject playerTagObject in playerTagObjects) {
            if (playerTagObject.transform.parent == null) {
                player = playerTagObject;
                break;
            }
        }

        return player;
    }

    public static Collider playerCollider()
    {
        var player = playerGameObject();

        if (player != null) {
            return player.GetComponentInChildren<Collider>();
        }

        return null;
    }
}