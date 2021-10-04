using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance Music;

    // Start is called before the first frame update
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Radio");
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
}
