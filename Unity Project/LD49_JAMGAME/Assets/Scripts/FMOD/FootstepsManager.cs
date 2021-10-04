using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] private string FootstepsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string JumpingEventPath;
    public string[] MaterialTypes;

    [SerializeField] private float RayDistance = 0.1f;
    [SerializeField] private float StepDistance = 2.0f;
    private float StepRandom;
    private Vector3 PrevPos;
    private float DistanceTravelled;
    private RaycastHit hit;
    private int F_MaterialValue;
    private bool PlayerTouchingGround;
    private bool PreviouslyTouchingGround;
    private float TimeTakenSinceStep;

    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        StepRandom = Random.Range(0f, 0.5f);
        PrevPos = transform.position;
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.blue);

        GroundedCheck();
        if (PlayerTouchingGround && Input.GetButtonDown("Jump"))
        {
            MaterialCheck();
            PlayJumpOrLand(true);
        }
        if (!PreviouslyTouchingGround && PlayerTouchingGround && player.jumpTime > 0.5f)
        {
            MaterialCheck();
            PlayJumpOrLand(false);
        }

        PreviouslyTouchingGround = PlayerTouchingGround;

        TimeTakenSinceStep += Time.deltaTime;
        DistanceTravelled += (transform.position - PrevPos).magnitude;
        if (DistanceTravelled >= StepDistance + StepRandom)
        {
            MaterialCheck();
            PlayFootstep();
            StepRandom = Random.Range(0f, 0.5f);
            DistanceTravelled = 0f;
        }
        PrevPos = transform.position;

    }

    void GroundedCheck()
    {
        PlayerTouchingGround = Physics.CheckCapsule(transform.position, transform.position - transform.up * RayDistance, 0.5f);
    }

    void MaterialCheck() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))                                 
        {
            if (hit.collider.gameObject.tag == "Carpet")
            { 
                F_MaterialValue = 1;    
            }
            else                                                                                                  
                F_MaterialValue = 0;                                                             
        }
        else                                                                                                         
            F_MaterialValue = 0;                                                                  
    }

    void PlayFootstep() 
    {
        if (PlayerTouchingGround)
        {
            FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);        
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());     
            Footstep.setParameterByName("Material", F_MaterialValue);                                                                             
            Footstep.start();                                                                                       
            Footstep.release();
        }
    }

    void PlayJumpOrLand(bool F_JumpLandCalc)
    {
        FMOD.Studio.EventInstance Jl = FMODUnity.RuntimeManager.CreateInstance(JumpingEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Jl, transform, GetComponent<Rigidbody>());
        Jl.setParameterByName("Material", F_MaterialValue);
        Jl.setParameterByName("JumpOrLand", F_JumpLandCalc ? 0f : 1f);
        Jl.start();
        Jl.release();
    }
}
