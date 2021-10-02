using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    public GameUI gameUI;

    // Object, fixed
    public List<TwoStatePointoutable> bugsInLevel = new List<TwoStatePointoutable>();
    public List<TwoStatePointoutable> fixedBugs = new List<TwoStatePointoutable>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleBugFixOrUnfix(TwoStatePointoutable bug)
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

    internal void RegisterBug(TwoStatePointoutable pointoutable)
    {
        bugsInLevel.Add(pointoutable);
        if (pointoutable.isCorrect)
        {
            fixedBugs.Add(pointoutable);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        gameUI.UpdateBugCounter(fixedBugs.Count, bugsInLevel.Count);

    }
}
