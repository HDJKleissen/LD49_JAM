using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBug : Bug
{
    [SerializeField] ElevatorMusic elevatorMusic;

    public override void DoStart()
    {
        if (elevatorMusic == null)
        {
            elevatorMusic = GetComponentInChildren<ElevatorMusic>();
        }
    }

    public override void DoUpdate()
    {
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
        elevatorMusic.SetAudioGlitchyness(IsFixed);
    }

}