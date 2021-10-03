using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBarkPlayer : MonoBehaviour
{
    [SerializeField] float ShortestTimeBetweenBarks = 15f;
    [SerializeField] float LongestTimeBetweenBarks = 25f;
    public bool isBug;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }


    void PlayBark()
    {
        if (isBug)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NPC_Bark_Bug", gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NPC_Bark", gameObject);
        }

        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }
}
