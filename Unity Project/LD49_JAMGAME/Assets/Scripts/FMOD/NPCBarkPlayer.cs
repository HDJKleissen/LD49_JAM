using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBarkPlayer : MonoBehaviour
{
    [SerializeField] float ShortestTimeBetweenBarks = 15f;
    [SerializeField] float LongestTimeBetweenBarks = 25f;
    public bool isBug;
    private NPC NPC;
    private NPCBug NPCBug;
    private FMOD.Studio.EventInstance VOLine;
    private FMOD.Studio.EventInstance BuggedVOLine;
    private FMOD.Studio.PLAYBACK_STATE VOLinePs;
    private FMOD.Studio.PLAYBACK_STATE BuggedVOLinePs;


    // Start is called before the first frame update
    void Start()
    {
        VOLine = FMODUnity.RuntimeManager.CreateInstance("event:/NPC_Bark");
        BuggedVOLine = FMODUnity.RuntimeManager.CreateInstance("event:/NPC_Bark_Bug");
        NPC = GetComponent<NPC>();
        NPCBug = GetComponent<NPCBug>();
        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }


    private void Update()
    {
        VOLine.getPlaybackState(out VOLinePs);
        BuggedVOLine.getPlaybackState(out BuggedVOLinePs);
        if(VOLinePs == FMOD.Studio.PLAYBACK_STATE.PLAYING || BuggedVOLinePs == FMOD.Studio.PLAYBACK_STATE.PLAYING )
        {
            if (NPCBug == null || !NPCBug.AnimationBugged || NPCBug.IsFixed)
            {
                NPC.SetTalking(true);
            }
        }
        else
        {
            if (NPCBug == null || !NPCBug.AnimationBugged || NPCBug.IsFixed)
            {
                NPC.SetTalking(false);
            }
        }
    }

    void PlayBark()
    {
        if (NPC.CanSitAndTalk || !NPC.sitting)
        {
            if (isBug)
            {
                BuggedVOLine.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
                BuggedVOLine.start();
            }
            else
            {
                VOLine.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
                VOLine.start();
            }

            Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
        }
    }

    private void OnDestroy()
    {
        VOLine.release();
        BuggedVOLine.release();
    }
}
