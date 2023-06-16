using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI soundValue;
    [SerializeField] private GameObject mainPanel;


    public void SoundSliderChange(float value)
    {
        soundValue.SetText($"{(value*100).ToString("N0")}");

        AudioListener.volume = value;
    }

    public void Return()
    {
        mainPanel.SetActive(true);

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        soundValue.SetText($"{(AudioListener.volume * 100).ToString("N0")}");
    }
}
