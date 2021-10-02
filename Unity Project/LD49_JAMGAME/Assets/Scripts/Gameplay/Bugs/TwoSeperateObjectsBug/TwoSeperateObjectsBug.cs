using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSeperateObjectsBug : Bug
{
    public TwoSeperateObjectsBugCorrect CorrectObject;
    public TwoSeperateObjectsBugIncorrect IncorrectObject;

    // Start is called before the first frame update
    public override void DoStart()
    {
        if (CorrectObject == null)
        {
            CorrectObject = GetComponentInChildren<TwoSeperateObjectsBugCorrect>();
        }
        if (IncorrectObject == null)
        {
            IncorrectObject = GetComponentInChildren<TwoSeperateObjectsBugIncorrect>();
        }
        CorrectObject.parent = this;
        IncorrectObject.parent = this;
    }

    public override void DoUpdate()
    {
    }

    public override void HandleToggle()
    {
        CorrectObject.gameObject.SetActive(!IsBugged || IsFixed);
        IncorrectObject.gameObject.SetActive(IsBugged && !IsFixed);
    }

    public override void HandleStartFix()
    {
        IncorrectObject.StartFixing();
    }

    public override void HandleStartBugging()
    {
        HandleToggle();
    }

    public override void HandleAttemptBehaviour()
    {
    }
}
