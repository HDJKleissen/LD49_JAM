using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointoutableIncorrect : MonoBehaviour
{
    public Pointoutable parent;
    // Start is called before the first frame update
    void Start()
    {
        if (parent == null)
        {
            parent = GetComponentInParent<Pointoutable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
