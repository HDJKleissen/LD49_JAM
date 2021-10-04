using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCState CurrentState, PreviousState;
    public Animator Animator;


    string BOOL_SITTING = "Sitting";
    string BOOL_WALKING = "Walking";
    string BOOL_TALKING = "Talking";
    string BOOL_TPOSE = "DoTPose";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != PreviousState)
        {
            SetAnimatorParams();
        }
        PreviousState = CurrentState;
    }

    void SetAnimatorParams()
    {
        switch (CurrentState)
        {
            case NPCState.Idle:
                Animator.SetBool(BOOL_SITTING, false);
                Animator.SetBool(BOOL_WALKING, false);
                Animator.SetBool(BOOL_TPOSE, false);
                Animator.SetBool(BOOL_TALKING, false);
                break;
            case NPCState.Walking:
                Animator.SetBool(BOOL_SITTING, false);
                Animator.SetBool(BOOL_WALKING, true);
                Animator.SetBool(BOOL_TPOSE, false);
                Animator.SetBool(BOOL_TALKING, false);
                break;
            case NPCState.SittingIdle:
                Animator.SetBool(BOOL_SITTING, true);
                Animator.SetBool(BOOL_WALKING, false);
                Animator.SetBool(BOOL_TPOSE, false);
                Animator.SetBool(BOOL_TALKING, false);
                break;
            case NPCState.SittingTalking:
                Animator.SetBool(BOOL_SITTING, true);
                Animator.SetBool(BOOL_WALKING, false);
                Animator.SetBool(BOOL_TPOSE, false);
                Animator.SetBool(BOOL_TALKING, true);
                break;
            case NPCState.Talking:
                Animator.SetBool(BOOL_SITTING, false);
                Animator.SetBool(BOOL_WALKING, false);
                Animator.SetBool(BOOL_TPOSE, false);
                Animator.SetBool(BOOL_TALKING, true);
                break;
            case NPCState.TPose:
                Animator.SetBool(BOOL_SITTING, false);
                Animator.SetBool(BOOL_WALKING, false);
                Animator.SetBool(BOOL_TPOSE, true);
                Animator.SetBool(BOOL_TALKING, false);
                break;
        }
    }
}

public enum NPCState
{
    Idle, 
    Walking,
    SittingIdle,
    SittingTalking,
    Talking, 
    TPose
}
