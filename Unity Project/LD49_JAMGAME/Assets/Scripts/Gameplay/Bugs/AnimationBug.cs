using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBug : Bug
{
    public Animator animator;
    
    public override void DoStart()
    {
    }

    public override void DoUpdate()
    {
    }

    public override void HandleAttemptBehaviour()
    {
        animator.SetTrigger("TryAnimation");
    }

    public override void HandleStartBugging()
    {
    }

    public override void HandleStartFix()
    {
    }

    public override void HandleToggle()
    {
        if (IsBugged)
        {
            animator.SetBool("IsFixed", IsFixed);
        }
    }
}
