using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOcclusionTrigger : MonoBehaviour
{
    private GameObject Player;
    private ElevatorMusic elevatorMusic;
    public bool IsBottomElevator = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        elevatorMusic = GetComponentInParent<ElevatorMusic>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player && !IsBottomElevator)
            elevatorMusic.SetDoorOpen(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player && !IsBottomElevator)
            elevatorMusic.SetDoorOpen(false);
    }
}
