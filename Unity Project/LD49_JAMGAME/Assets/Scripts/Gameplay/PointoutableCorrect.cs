using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointoutableCorrect : MonoBehaviour
{
    public TwoStatePointoutable parent;
    // Start is called before the first frame update
    void Start()
    {
        if(parent == null)
        {
            parent = GetComponentInParent<TwoStatePointoutable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
