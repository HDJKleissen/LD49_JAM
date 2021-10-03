using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ProximityTrigger : MonoBehaviour
{
    public UnityEvent EnterAction, StayAction, ExitAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnterAction.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StayAction.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ExitAction.Invoke();
        }
    }

    private void OnValidate()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
}
