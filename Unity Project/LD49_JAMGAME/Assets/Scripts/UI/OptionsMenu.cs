using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TextMeshProUGUI MusicText, MouseSensitivityText;
    public Slider MusicSlider, MouseSensitivitySlider;
    public static string MUSIC_LEVEL = "MusicLevel";
    public static string MOUSE_SENSITIVITY = "MouseSensitivity";

    // Start is called before the first frame update
    void Start()
    {
        float MusicSliderValue = PlayerPrefs.GetFloat(MUSIC_LEVEL, 0.5f);
        float MouseSensitivitySliderValue = PlayerPrefs.GetFloat(MOUSE_SENSITIVITY, 1);

        MusicSlider.value = MusicSliderValue;
        
        MouseSensitivitySlider.value = MouseSensitivitySliderValue;
        OnMusicSliderValueChange();
        OnMouseSensitivitySliderValueChange();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMusicSliderValueChange()
    {
        float value = (float)Math.Round(MusicSlider.value, 2);

        // Change FMOD values here?
        PlayerPrefs.SetFloat(MUSIC_LEVEL, value);
        MusicText.SetText($"Sound Level: {value}");
    }

    public void OnMouseSensitivitySliderValueChange()
    {
        float value = (float)Math.Round(MouseSensitivitySlider.value, 2);

        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY, value);
        GameManager.MouseSensitivity = value;
        MouseSensitivityText.SetText($"Mouse Sensitivity: {value}");
    }
}

