using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Footsteps : MonoBehaviour
{
    public void PlayFootstep(int isBug)
    {
        if (isBug == 1)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NPC_Footstep_Bug", gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NPC_Footstep", gameObject);
        }
    }
}
