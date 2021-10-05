using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMusicIsLoaded : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Music_Bank"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            StartCoroutine(CoroutineHelper.Chain(
                CoroutineHelper.WaitUntil(() => FMODUnity.RuntimeManager.HasBankLoaded("Master")),
                CoroutineHelper.WaitUntil(() => FMODUnity.RuntimeManager.HasBankLoaded("Music")),
                CoroutineHelper.WaitUntil(() => FMODUnity.RuntimeManager.HasBankLoaded("SFX")),
                CoroutineHelper.Do(() => SceneManager.LoadScene("MainMenu"))
            ));
        }
    }
}