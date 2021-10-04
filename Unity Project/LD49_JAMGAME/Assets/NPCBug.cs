using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBug : Bug
{
    public NPC NPC;
    public NPCBarkPlayer NPCAudio;
    public NPCState IncorrectBool, CorrectBool;
    public bool AudioBugged = false;

    public override void DoStart()
    {
        if (NPC == null)
        {
            NPC = GetComponent<NPC>();
        }
        if (NPCAudio == null)
        {
            NPCAudio = GetComponent<NPCBarkPlayer>();
        }
        NPCAudio.isBug = AudioBugged;

        if (IsBugged && !IsFixed)
        {
            if(IncorrectBool == NPCState.BOOL_IDLE)
            {
                NPC.SetSitting(false);
                NPC.SetWalking(false);
                NPC.SetTalking(false);
                NPC.SetTPose(false);
            }
            else
            {
                NPC.SetSitting(IncorrectBool == NPCState.BOOL_SITTING);
                NPC.SetWalking(IncorrectBool == NPCState.BOOL_WALKING);
                NPC.SetTalking(IncorrectBool == NPCState.BOOL_TALKING);
                NPC.SetTPose(IncorrectBool == NPCState.BOOL_TPOSE);
            }
        }
    }

    public override void DoUpdate()
    {
    }

    public override void HandleAttemptBehaviour()
    {
    }

    public override void HandleStartBugging()
    {
    }

    public override void HandleStartFix()
    {
    }

    public override void HandleToggle()
    {
        if (IsFixed)
        {
            if(CorrectBool == NPCState.BOOL_IDLE)
            {
                NPC.SetSitting(false);
                NPC.SetWalking(false);
                NPC.SetTalking(false);
                NPC.SetTPose(false);
            }
            else
            {
                NPC.SetSitting(CorrectBool == NPCState.BOOL_SITTING);
                NPC.SetWalking(CorrectBool == NPCState.BOOL_WALKING);
                NPC.SetTalking(CorrectBool == NPCState.BOOL_TALKING);
                NPC.SetTPose(CorrectBool == NPCState.BOOL_TPOSE);
            }

            NPCAudio.isBug = false;
        }
    }
}
