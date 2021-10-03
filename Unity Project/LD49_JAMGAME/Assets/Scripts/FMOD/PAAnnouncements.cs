using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAAnnouncements : MonoBehaviour
{
    [SerializeField] float TimeBeforeFirstAnnouncement;
    [SerializeField] float ShortestTimeBetweenAnnouncements;
    [SerializeField] float LongestTimeBetweenAnnouncements;
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

            //if we don't need to update location could use FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PA_Bug", gameObject);
        }
        else
        {
            NormalLine.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
            NormalLine.start();

            //if we don't need to update location could use FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PA", gameObject);
        }

        Invoke("PlayPALine", Random.Range(ShortestTimeBetweenAnnouncements, LongestTimeBetweenAnnouncements));
    }

    void OnDestroy()
    {
        BugLine.release();
        NormalLine.release();
    }
}
