using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneBig : MonoBehaviour
{
    FMOD.Studio.EventInstance Reverb;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Reverb = FMODUnity.RuntimeManager.CreateInstance("snapshot:/ReverbZoneBig");
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
            Reverb.start();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
            Reverb.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void OnDestroy()
    {
        Reverb.release();
    }
}
