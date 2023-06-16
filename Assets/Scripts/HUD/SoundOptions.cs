using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI soundValue;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Slider slider;



    public void SoundSliderChange(float value)
    {
        soundValue.SetText($"{(value*100).ToString("N0")}");

        AudioListener.volume = value;
    }

    public void Return()
    {
        if(mainPanel != null)
        mainPanel.SetActive(true);

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        slider.value = AudioListener.volume;
        soundValue.SetText($"{(AudioListener.volume * 100).ToString("N0")}");
    }
}
