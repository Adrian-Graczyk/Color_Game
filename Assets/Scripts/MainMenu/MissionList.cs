using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionList : MonoBehaviour 
{
    private void Start() 
    {
        SaveData saveData = SaveSystem.LoadProgress();

        setChildVisible(saveData.currentSceneIndex);
    }

    private void setChildVisible(int index)
    {
        int childCount = transform.childCount;
    
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(i < index);
        }
    }
}
