using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IHighlightable
{
    public NPCState CurrentState, PreviousState;
    public Animator Animator;
    public Renderer[] Parts;

    string BOOL_SITTING = "Sitting";
    string BOOL_WALKING = "Walking";
    string BOOL_TALKING = "Talking";
    string BOOL_TPOSE = "DoTPose";

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }

    public Color highlightColor => Constants.HIGHLIGHT_COLOR;

    // Start is called before the first frame update
    void Start()
    {
        Parts = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in Parts)
        {
            renderer.material.SetColor("_EmissionColor", highlightColor);
        }
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

    public void ToggleHighlight(bool highlighting)
    {
        Debug.Log("Togglin");
        foreach(Renderer renderer in Parts)
        {
            if (highlighting)
            {
                renderer.material.EnableKeyword("_EMISSION");
            }
            else
            {
                renderer.material.DisableKeyword("_EMISSION");
            }
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
