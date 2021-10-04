using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Elevator MovingElevator, LinkedElevator;
    public Animator animator;
    public float MoveSpeed;

    public ElevatorMusic elevatorMusic;
    private FMOD.Studio.EventInstance rideSound;

    bool isOpen = false;
    bool isDown = false;
    bool movingDown = false, movingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        if (elevatorMusic == null)
        {
            elevatorMusic = GetComponent<ElevatorMusic>();
        }
        rideSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElevatorRide");
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDown)
        {
            transform.position -= new Vector3(0, MoveSpeed * Time.deltaTime, 0);
        }
        else if (movingUp)
        {
            transform.position += new Vector3(0, MoveSpeed * Time.deltaTime, 0);
        }

    }

    public void StartMoveDown()
    {
        movingDown = true;
    }
    public void StopMoveDown()
    {
        movingDown = false;
    }
    public void StartMoveUp()
    {
        movingUp = true;
    }
    public void StopMoveUp()
    {
        movingUp = false;
    }

    public void ToggleDoors()
    {
        if(!isOpen)
        {
            OpenDoors();
        }
        else
        {
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        animator.ResetTrigger("CloseDoors");
        isOpen = true;
        animator.SetTrigger("OpenDoors");
        if (LinkedElevator != null)
        {
            LinkedElevator.isOpen = true;
            LinkedElevator.animator.SetTrigger("OpenDoors");
        }
        elevatorMusic.SetDoorOpen(isOpen);
    }

    public void CloseDoors()
    {
        animator.ResetTrigger("OpenDoors");
        isOpen = false;
        animator.SetTrigger("CloseDoors");
        if (LinkedElevator != null)
        {
            LinkedElevator.isOpen = false;
            LinkedElevator.animator.SetTrigger("CloseDoors");
        }
        elevatorMusic.SetDoorOpen(isOpen);
    }

    public void MoveElevator()
    {
        if (MovingElevator != null)
        {
            Vector3 playerToElevatorDiff = transform.position - GameManager.Instance.player.transform.position;

            GameManager.Instance.player.CharacterController.enabled = false;
            GameManager.Instance.player.transform.position = MovingElevator.transform.position - playerToElevatorDiff;
            GameManager.Instance.player.transform.parent = MovingElevator.transform;
            MovingElevator.animator.SetTrigger("MoveElevator");
            if (MovingElevator.isDown)
            {
                MovingElevator.StartMoveUp();
            }
            else
            {
                MovingElevator.StartMoveDown();
            }

            // Start Elevator Rumble
            rideSound.start();

            StartCoroutine(CoroutineHelper.DelaySeconds(() => MovingElevator.animator.SetTrigger("StopMoveElevator"), 5));
            StartCoroutine(CoroutineHelper.DelaySeconds(() =>
            {
                // Stop Elevator Rumble
                rideSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                MovingElevator.isDown = !MovingElevator.isDown;
                MovingElevator.StopMoveDown();
                MovingElevator.StopMoveUp();
                GameManager.Instance.player.transform.parent = null;
                Vector3 playerToElevatorDiff = MovingElevator.transform.position - GameManager.Instance.player.transform.position;
                GameManager.Instance.player.transform.position = LinkedElevator.transform.position - playerToElevatorDiff;
                GameManager.Instance.player.CharacterController.enabled = true;
            }, 10));
            StartCoroutine(CoroutineHelper.DelaySeconds(() => LinkedElevator.OpenDoors(), 11));
        }
    }

    private void OnDestroy()
    {
        rideSound.release();
    }
}

//elev: 100,100,100
//    player: 80,80,80
//    diff: 20,20,20
//    movingelev: 100,50,100
//    newplaypos: 80,30,80