using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    private FMOD.Studio.EventInstance Ambience;

    // Start is called before the first frame update
    void Start()
    {
        Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Ambience, transform);
        Ambience.start();
        Ambience.release();
    }

    private void OnDestroy()
    {
        Ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
