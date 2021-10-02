using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBug : Bug
{
    // reference incorrect and correct sound to be played (or just the fmod event idk how you're switching this)

    public override void DoStart()
    {
        // Leave empty
    }

    public override void DoUpdate()
    {
        // Distance attenuation, etc here
    }

    public override void HandleStartFix()
    {
        // Leave empty, or stop sound here if you want
    }

    public override void HandleToggle()
    {
        // Switch audio file/change FMOD values with:

        bool ThisVariableContainsWhetherTheBugIsFixed = IsFixed;
    }

}
