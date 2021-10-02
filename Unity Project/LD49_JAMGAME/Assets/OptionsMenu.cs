using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TextMeshProUGUI MusicText, SFXText, MouseSensitivityText;
    public Slider MusicSlider, SFXSlider, MouseSensitivitySlider;
    public static string MUSIC_LEVEL = "MusicLevel";
    public static string SFX_LEVEL = "SFXLevel";
    public static string MOUSE_SENSITIVITY = "MouseSensitivity";

    // Start is called before the first frame update
    void Start()
    {
        float MusicSliderValue = PlayerPrefs.GetFloat(MUSIC_LEVEL, 0.5f);
        float SFXSliderValue = PlayerPrefs.GetFloat(SFX_LEVEL, 0.5f);
        float MouseSensitivitySliderValue = PlayerPrefs.GetFloat(MOUSE_SENSITIVITY, 1);

        MusicSlider.value = MusicSliderValue;
        SFXSlider.value = SFXSliderValue;
        MouseSensitivitySlider.value = MouseSensitivitySliderValue;
        OnMusicSliderValueChange();
        OnSFXSliderValueChange();
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
        MusicText.SetText($"Music: {value}");
    }

    public void OnSFXSliderValueChange()
    {
        float value = (float)Math.Round(SFXSlider.value, 2);

        // Change FMOD values here?
        PlayerPrefs.SetFloat(SFX_LEVEL, value);
        SFXText.SetText($"Sound Effects: {value}");
    }

    public void OnMouseSensitivitySliderValueChange()
    {
        float value = (float)Math.Round(MouseSensitivitySlider.value, 2);

        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY, value);
        GameManager.MouseSensitivity = value;
        MouseSensitivityText.SetText($"Mouse Sensitivity: {value}");
    }
}

