using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    public GameUI gameUI;
    public EndingController endingController;

    public bool ElevatorMusicIsFixed = false;

    public List<Bug> bugsInLevel = new List<Bug>();
    public List<Bug> fixedBugs = new List<Bug>();
    public int MaxBugFixFailures;
    public bool IsPaused = false, IsEnding = false;

    public int bugFixFailures = 0;

    public PlayerController player;

    static float _mouseSensitivity = -1f;
    public static float MouseSensitivity {
        get {
            if(_mouseSensitivity < 0)
            {
                _mouseSensitivity = PlayerPrefs.GetFloat(OptionsMenu.MOUSE_SENSITIVITY);
            }
            return _mouseSensitivity;
        }
        set {
            _mouseSensitivity = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameUI.InitMaxFailures(MaxBugFixFailures);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartEnding();
        }
    }

    public void HandleBugToggleFix(Bug bug)
    {
        if (fixedBugs.Contains(bug))
        {
            fixedBugs.Remove(bug);
        }
        else
        {
            fixedBugs.Add(bug);
        }
    }

    internal void RegisterBug(Bug pointoutable)
    {
        bugsInLevel.Add(pointoutable);
        if (pointoutable.IsFixed)
        {
            fixedBugs.Add(pointoutable);
        }
    }

    internal void DisableScanUI()
    {
        gameUI.DisableScanUI();
    }

    internal void UpdateScanningUI(float scanTime, float maxScanTime)
    {
        gameUI.UpdateScanningUI(scanTime/maxScanTime);
    }

    internal void BugReportFailure()
    {
        bugFixFailures++;
        gameUI.AddFailureImage();
        if (bugFixFailures >= MaxBugFixFailures)
        {
            StartEnding();
            FMODUnity.RuntimeManager.PlayOneShot("event:/You_Are_Fired");
        }
    }

    internal void ShowHint(string text, float showTime)
    {
        gameUI.ShowHint(text, showTime);
    }

    public void StartEnding()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsEnding = true;
        gameUI.gameObject.SetActive(false);
        endingController.StartEnding();
    }
}
