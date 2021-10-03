using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAAnnouncements : MonoBehaviour
{
    [SerializeField] float TimeBeforeFirstAnnouncement = 3f;
    [SerializeField] float ShortestTimeBetweenAnnouncements = 10f;
    [SerializeField] float LongestTimeBetweenAnnouncements = 15f;
    public bool isBug;
    FMOD.Studio.EventInstance NormalLine;
    FMOD.Studio.EventInstance BugLine;


    // Start is called before the first frame update
    void Start()
    {
        NormalLine = FMODUnity.RuntimeManager.CreateInstance("event:/PA");
        BugLine = FMODUnity.RuntimeManager.CreateInstance("event:/PA_Bug");
        Invoke("PlayPALine", TimeBeforeFirstAnnouncement);
    }
     

    void PlayPALine()
    {
        if(isBug)
        {
            BugLine.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
            BugLine.start();
            BugLine.release();
            //if we don't need to update location could use FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PA_Bug", gameObject);
        }
        else
        {
            NormalLine.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
            NormalLine.start();
            NormalLine.release();
            //if we don't need to update location could use FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PA", gameObject);
        }

        Invoke("PlayPALine", Random.Range(ShortestTimeBetweenAnnouncements, LongestTimeBetweenAnnouncements));
    }
}
