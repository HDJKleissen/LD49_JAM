using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMusicTrigger : MonoBehaviour
{
    private GameObject Player;
    private ElevatorMusic elevatorMusic;



    private void Start()
    {
        Player = GameObject.Find("Player");
        elevatorMusic = GetComponentInParent<ElevatorMusic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            elevatorMusic.SetHouseMusic(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            elevatorMusic.SetHouseMusic(false);
        }

    }
}