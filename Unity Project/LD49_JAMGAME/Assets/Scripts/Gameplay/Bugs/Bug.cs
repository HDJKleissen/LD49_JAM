using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bug : MonoBehaviour, IFixable
{
    public Transform FixingParticlesLocation;
    public Transform FixTimeCircleLocation;

    [SerializeField] float scanTime;

    [SerializeField] bool isBugged = true;
    [SerializeField] bool isFixed = false;

    bool isFixing = false;
    public float MaxFixTime;

    GameObject particlesObject;
    FixTimeCircle fixTimeCircle;
    float fixTime = 0;
    public Collider BugCollider;
    public bool IsFixed { get => isFixed; set => isFixed = value; }
    public bool IsFixing { get => isFixing; set => isFixing = value; }
    public bool IsBugged { get => isBugged; set => isBugged = value; }
    //public float ScanTime => scanTime;

    // Start is called before the first frame update
    void Start()
    {
        DoStart();
        if (IsBugged)
        {
            // Layer 3 is bug layer
            gameObject.layer = 3;
            if (BugCollider != null)
            {
                BugCollider.gameObject.layer = 3;
            }
            GameManager.Instance.RegisterBug(this);
        }
        if (FixingParticlesLocation == null)
        {
            FixingParticlesLocation = transform;
        }
        if (FixTimeCircleLocation == null)
        {
            FixTimeCircleLocation = new GameObject().transform;
            FixTimeCircleLocation.parent = transform;
            FixTimeCircleLocation.localPosition = new Vector3(0, 3, 0);
        }

        HandleToggle();
    }

    public abstract void DoStart();

    // Update is called once per frame
    void Update()
    {
        DoUpdate();
        if (isFixing)
        {
            fixTime += Time.deltaTime;
            fixTimeCircle.UpdateCircleFillAmount(fixTime / MaxFixTime);

            if (fixTime >= MaxFixTime)
            {
                FixObject();
            }
        }
    }

    public abstract void DoUpdate();
    
    public void StartBugging()
    {
        if (!IsBugged)
        {
            // Layer 3 is bug layer
            gameObject.layer = 3;
            if (BugCollider != null)
            {
                BugCollider.gameObject.layer = 3;
            }
            IsBugged = true;
            HandleStartBugging();
            GameManager.Instance.RegisterBug(this);
        }
    }

    public void StartFix()
    {
        if (MaxFixTime > 0)
        {
            fixTime = 0;
            isFixing = true;
            particlesObject = Instantiate(BugPrefabs.Instance.FixParticlesPrefab, FixingParticlesLocation);
            fixTimeCircle = Instantiate(BugPrefabs.Instance.FixTimeCirclePrefab, FixTimeCircleLocation).GetComponent<FixTimeCircle>();
            HandleStartFix();
        }
        else
        {
            FixObject();
        }
    }

    public void FixObject()
    {
        if (particlesObject != null)
        {
            Destroy(particlesObject);
        }
        if (fixTimeCircle != null)
        {
            Destroy(fixTimeCircle.gameObject);
        }

        isFixing = false;
        IsFixed = true;
        IsBugged = IsFixed;
        HandleToggle();
        GameManager.Instance.HandleBugToggleFix(this);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Bug_Fixed");
    }

    public void AttemptBehaviour()
    {
        HandleAttemptBehaviour();
    }

    public abstract void HandleToggle();
    public abstract void HandleStartFix();
    public abstract void HandleStartBugging();
    public abstract void HandleAttemptBehaviour();
}