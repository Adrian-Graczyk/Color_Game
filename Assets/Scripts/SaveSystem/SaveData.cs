using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentSceneIndex = 3;

    public SaveData(int currentSceneIndex = 3)
    {
        this.currentSceneIndex = currentSceneIndex; 
    }
}
