using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBug : Bug
{
    ElevatorMusic elevatorMusic;

    public override void DoStart()
    {
        elevatorMusic = GetComponent<ElevatorMusic>();
    }

    public override void DoUpdate()
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
