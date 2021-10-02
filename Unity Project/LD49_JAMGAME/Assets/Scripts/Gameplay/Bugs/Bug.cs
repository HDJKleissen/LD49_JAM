using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bug : MonoBehaviour
{
    public Transform FixingParticlesLocation;
    public Transform FixTimeCircleLocation;
    public float ScanTime;

    public bool IsBugged = true;
    public bool IsFixed = false, isFixing = false;
    public float MaxFixTime;

    GameObject particlesObject;
    FixTimeCircle fixTimeCircle;
    float fixTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        DoStart();
        if (IsBugged)
        {
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
    }

    public abstract void HandleToggle();
    public abstract void HandleStartFix();
    public abstract void HandleStartBugging();
}