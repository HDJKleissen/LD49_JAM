using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBug : Bug, IHighlightable
{
    public ElevatorMusic elevatorMusic;

    [SerializeField] RadioMusic radioMusic;

    public Renderer Renderer;

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }
    public Color highlightColor => Constants.HIGHLIGHT_COLOR;

    Texture originalEmissionMap;

    public override void DoStart()
    {
        if (Renderer == null)
        {
            Renderer = GetComponent<Renderer>();
        }
        if (elevatorMusic == null)
        {
            elevatorMusic = GetComponentInChildren<ElevatorMusic>();
        }
        if (radioMusic == null)
        {
            radioMusic = GetComponentInChildren<RadioMusic>();
        }
        originalEmissionMap = Renderer.materials[0].GetTexture("_EmissionMap");
    }

    public override void DoUpdate()
    {
        if (IsFixed != GameManager.Instance.ElevatorMusicIsFixed)
        {
            IsFixed = GameManager.Instance.ElevatorMusicIsFixed;
        }
    }

    public override void HandleAttemptBehaviour()
    {
    }

    public override void HandleStartBugging()
    {
        HandleToggle();
    }

    public override void HandleStartFix()
    {
    }

    public override void HandleToggle()
    {
        // Switch audio file/change FMOD values with:
        if (elevatorMusic != null)
        {
            GameManager.Instance.ElevatorMusicIsFixed = IsFixed;
            elevatorMusic.SetAudioGlitchyness(IsFixed);
        }
        else if (radioMusic != null)
        {
            radioMusic.SetAudioGlitchyness(IsFixed);
        }
    }

    public void ToggleHighlight(bool highlighting)
    {
        if (highlighting && !IsFixing)
        {
            if (tag == "HasEmissionMap")
            {
                Renderer.materials[0].SetTexture("_EmissionMap", null);
            }
            else
            {
                Renderer.materials[0].EnableKeyword("_EMISSION");
            }
            Renderer.materials[0].SetColor("_EmissionColor", highlightColor);
        }
        else 
        {
            if (tag == "HasEmissionMap")
            {
                Renderer.materials[0].SetTexture("_EmissionMap", originalEmissionMap);
            }
            else
            {
                Renderer.materials[0].DisableKeyword("_EMISSION");
            }
            Renderer.materials[0].SetColor("_EmissionColor", OriginalColor);
        }
    }
}
