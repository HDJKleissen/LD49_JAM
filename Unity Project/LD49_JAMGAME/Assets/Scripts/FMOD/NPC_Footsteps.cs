using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Footsteps : MonoBehaviour
{
    public bool isBug;

    public void PlayFootstep()
    {
        if (isBug)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NPC_Footstep_Bug", gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NPC_Footstep", gameObject);
        }
    }
}
