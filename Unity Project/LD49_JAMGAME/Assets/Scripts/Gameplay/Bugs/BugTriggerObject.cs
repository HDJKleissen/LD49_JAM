using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTriggerObject : MonoBehaviour
{
    public Bug LinkedBug;
    public void TriggerBug()
    {
        LinkedBug.StartBugging();
    }
}
