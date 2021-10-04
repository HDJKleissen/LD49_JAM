using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObject;

    bool isPaused = false;

    FMOD.Studio.EventInstance pauseSnapshot;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPausedState(!isPaused);
        }
    }

    public void SetPausedState(bool paused)
    {
        isPaused = paused;
        GameManager.Instance.IsPaused = paused;
        PauseMenuObject.SetActive(isPaused);
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            pauseSnapshot.start();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            pauseSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnDestroy()
    {
        pauseSnapshot.release();
    }
}
