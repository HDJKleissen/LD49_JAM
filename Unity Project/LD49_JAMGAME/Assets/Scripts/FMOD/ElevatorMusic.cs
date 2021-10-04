using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance Music;
    bool localFixedValue = false;
    // Start is called before the first frame update
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Elevator_Music");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Music, transform);
        Music.start();
        Music.release();
        SetAudioGlitchyness(localFixedValue);
    }

    private void Update()
    {
        if (GameManager.Instance.ElevatorMusicIsFixed != localFixedValue)
        {
            SetAudioGlitchyness(GameManager.Instance.ElevatorMusicIsFixed);
            localFixedValue = GameManager.Instance.ElevatorMusicIsFixed;
        }
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetAudioGlitchyness(bool isFixed)
    {
        localFixedValue = isFixed;
        Music.setParameterByName("Glitch", isFixed ? 0 : 1);
    }

    public void SetDoorOpen(bool isOpen)
    {
        Music.setParameterByName("DoorOpen", isOpen ? 1f : 0f);
    }

    public void SetHouseMusic(bool isHouse)
    {
        Music.setParameterByName("House", isHouse ? 1f : 0f);
    }
}