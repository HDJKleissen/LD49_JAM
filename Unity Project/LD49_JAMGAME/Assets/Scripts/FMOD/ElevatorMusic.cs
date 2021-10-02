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
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Music, transform, GetComponent<Rigidbody>());
        Music.start();
        Music.release();
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void FixAudioGlitch()
    {
        Music.setParameterByName("Glitch", 0);
    }
}
