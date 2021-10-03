using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef]
    public string EventPath;
    private FMOD.Studio.EventInstance Ambience;

    // Start is called before the first frame update
    void Start()
    {
        Ambience = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Ambience, transform);
        Ambience.start();
        Ambience.release();
    }

    private void OnDestroy()
    {
        Ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
