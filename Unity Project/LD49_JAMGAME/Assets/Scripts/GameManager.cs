using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    public GameUI gameUI;

    public List<Bug> bugsInLevel = new List<Bug>();
    public List<Bug> fixedBugs = new List<Bug>();

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

    }

    // Update is called once per frame
    void Update()
    {

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
        UpdateUI();
    }

    internal void RegisterBug(Bug pointoutable)
    {
        bugsInLevel.Add(pointoutable);
        if (pointoutable.IsFixed)
        {
            fixedBugs.Add(pointoutable);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        gameUI.UpdateBugCounter(fixedBugs.Count, bugsInLevel.Count);

    }

    internal void DisableScanUI()
    {
        gameUI.DisableScanUI();
    }

    internal void UpdateScanningUI(float scanTime, float maxScanTime)
    {
        gameUI.UpdateScanningUI(scanTime/maxScanTime);
    }
}
