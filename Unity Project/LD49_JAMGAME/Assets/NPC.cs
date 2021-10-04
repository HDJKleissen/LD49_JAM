using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IHighlightable
{
    public static Dictionary<NPCState, string> EnumToBoolname = new Dictionary<NPCState, string> {
        { NPCState.BOOL_SITTING, BOOL_SITTING },
        { NPCState.BOOL_WALKING, BOOL_WALKING },
        { NPCState.BOOL_TALKING, BOOL_TALKING },
        { NPCState.BOOL_TPOSE, BOOL_TPOSE }
    };
    public bool CanSitAndTalk = true;
    public bool sitting, walking, talking, tPose;
    public Animator Animator;
    public Renderer[] Parts;

    static string BOOL_SITTING = "Sitting";
    static string BOOL_WALKING = "Walking";
    static string BOOL_TALKING = "Talking";
    static string BOOL_TPOSE = "DoTPose";

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }

    public Color highlightColor => Constants.HIGHLIGHT_COLOR;

    // Start is called before the first frame update
    void Start()
    {
        Parts = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in Parts)
        {
            renderer.materials[0].SetColor("_EmissionColor", highlightColor);
        }
        SetAnimatorParams();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetAnimatorParams()
    {
        Animator.SetBool(BOOL_SITTING, sitting);
        Animator.SetBool(BOOL_WALKING, walking);
        Animator.SetBool(BOOL_TPOSE, tPose);
        Animator.SetBool(BOOL_TALKING, talking);
    }

    public void SetTalking(bool newTalking)
    {
        if (CanSitAndTalk || !sitting)
        {
            talking = newTalking;
            SetAnimatorParams();
        }
    }
    public void SetWalking(bool newWalking)
    {
        walking = newWalking;
        SetAnimatorParams();
    }
    public void SetSitting(bool newSitting)
    {
        sitting = newSitting;
        SetAnimatorParams();
    }
    public void SetTPose(bool newTPose)
    {
        tPose = newTPose;
        SetAnimatorParams();
    }

    public void ToggleHighlight(bool highlighting)
    {
        foreach(Renderer renderer in Parts)
        {
            if (highlighting)
            {
                renderer.materials[0].EnableKeyword("_EMISSION");
            }
            else
            {
                renderer.materials[0].DisableKeyword("_EMISSION");
            }
        }
    }
}

public enum NPCState
{
    BOOL_IDLE,
    BOOL_SITTING,
    BOOL_WALKING,
    BOOL_TALKING,
    BOOL_TPOSE
}
