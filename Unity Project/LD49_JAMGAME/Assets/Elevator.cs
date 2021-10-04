using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Elevator MovingElevator, LinkedElevator;
    public Animator animator;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleDoors()
    {
        isOpen = !isOpen;
        if(isOpen)
        {
            OpenDoors();
        }
        else
        {
            CloseDoors();
        }
    }

    void OpenDoors()
    {
        animator.SetTrigger("OpenDoors");
        LinkedElevator.animator.SetTrigger("OpenDoors");
    }

    void CloseDoors()
    {
        animator.SetTrigger("CloseDoors");
        LinkedElevator.animator.SetTrigger("CloseDoors");
    }

    public void MoveElevator()
    {
        Vector3 diffToPlayer = GameManager.Instance.player.transform.position - transform.position;

        GameManager.Instance.player.transform.position = MovingElevator.transform.position - diffToPlayer;

        MovingElevator.animator.SetTrigger("MoveElevator");
    }
}
