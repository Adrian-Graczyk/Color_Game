using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentSceneIndex = 1;

    public SaveData(int currentSceneIndex = 1)
    {
        this.currentSceneIndex = currentSceneIndex; 
    }
}