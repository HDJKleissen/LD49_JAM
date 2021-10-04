using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroVO : MonoBehaviour
{

    FMOD.Studio.EventInstance VO;

    // Start is called before the first frame update
    void Start()
    {
        VO = FMODUnity.RuntimeManager.CreateInstance("event:/Intro_VO");
        VO.start();
        VO.release();
    }

    public void StopVO()
    {
        VO.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
