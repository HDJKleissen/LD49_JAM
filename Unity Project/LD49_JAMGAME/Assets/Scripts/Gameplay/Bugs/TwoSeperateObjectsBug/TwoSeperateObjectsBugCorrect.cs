using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSeperateObjectsBugCorrect : MonoBehaviour
{
    public TwoSeperateObjectsBug parent;
    // Start is called before the first frame update
    void Start()
    {
        if(parent == null)
        {
            parent = GetComponentInParent<TwoSeperateObjectsBug>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
