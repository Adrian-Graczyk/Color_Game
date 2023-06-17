using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentSceneIndex = 2;

    public SaveData(int currentSceneIndex = 2)
    {
        this.currentSceneIndex = currentSceneIndex; 
    }
}
