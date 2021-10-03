using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{

    public void PlayConfirmSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Confirm");
    }

    public void PlayBackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Back");
    }
}
