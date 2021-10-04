using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance Music;

    // Start is called before the first frame update
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Elevator_Music");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Music, transform);
        Music.start();
        Music.release();
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetAudioGlitchyness(bool isFixed)
    {
        Music.setParameterByName("Glitch", isFixed ? 0 : 1);
    }

    public void SetDoorOpen(bool isOpen)
    {
        Music.setParameterByName("DoorOpen", isOpen ? 1f : 0f);
    }
}