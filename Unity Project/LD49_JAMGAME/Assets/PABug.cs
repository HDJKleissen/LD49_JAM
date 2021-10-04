using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PABug : Bug
{
    PAAnnouncements pa;
    public override void DoStart()
    {
        pa = GetComponent<PAAnnouncements>();
    }

    public override void DoUpdate()
    {
    }

    public override void HandleAttemptBehaviour()
    {
    }

    public override void HandleStartBugging()
    { }

    public override void HandleStartFix()
    {
    }

    public override void HandleToggle()
    {
        pa.isBug = !IsFixed;
    }
}
