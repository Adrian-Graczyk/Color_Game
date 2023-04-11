using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MissionBriefing : MonoBehaviour
{
    public BriefingScreenData briefingScreenData;
    public TextMeshProUGUI description;
    public Image captainImage;
    public Image missionImage;

    public void Start() {
        loadScreenData();
    }

    public void startMission() {
        SceneManager.LoadScene(briefingScreenData.scene.name);
    }

    public void loadMission(BriefingScreenData data) {
        briefingScreenData = data;
        loadScreenData();
    }

    private void loadScreenData() {
        description.text = briefingScreenData.missionDescription.text;
        captainImage.sprite = createSprite(briefingScreenData.captainThumbnail);
        missionImage.sprite = createSprite(briefingScreenData.missionThumbnail);
    }

    private Sprite createSprite(Texture2D texture) {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
