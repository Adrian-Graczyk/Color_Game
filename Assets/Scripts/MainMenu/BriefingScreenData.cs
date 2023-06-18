using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


[CreateAssetMenu(fileName = "BriefingScreenData", menuName = "Color_Game/BriefingScreenData", order = 0)]
public class BriefingScreenData : ScriptableObject {
    public Texture2D missionThumbnail;
    public Texture2D captainThumbnail;
    public Texture2D rebelThumbnail;
    public TextAsset missionDescription;
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
