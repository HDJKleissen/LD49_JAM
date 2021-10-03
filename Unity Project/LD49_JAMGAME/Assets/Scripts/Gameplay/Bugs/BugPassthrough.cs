using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPassthrough : MonoBehaviour, IFixable
{
    public Bug LinkedBug;

    public bool IsFixed { get => LinkedBug.IsFixed; set => LinkedBug.IsFixed = value; }
    public bool IsFixing { get => LinkedBug.IsFixing; set => LinkedBug.IsFixing = value; }
    public bool IsBugged { get => LinkedBug.IsBugged; set => LinkedBug.IsBugged = value; }

    //public float ScanTime => LinkedBug.ScanTime;

    public void StartFix()
    {
        LinkedBug.StartFix();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
