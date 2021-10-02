using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bug : MonoBehaviour
{
    public GameObject FixingParticlesObject;
    public Image FixingTimeImage;

    public float ScanTime;

    public bool IsBugged = true;
    public bool IsFixed = false, isFixing = false;
    public float MaxFixTime;
    float fixTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        DoStart();
        if (IsBugged)
        {
            GameManager.Instance.RegisterBug(this);
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
            FixingTimeImage.fillAmount = fixTime / MaxFixTime;

            if (fixTime >= MaxFixTime)
            {
                ToggleObject();
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
            FixingParticlesObject.SetActive(true);
            FixingTimeImage.gameObject.SetActive(true);
            HandleStartFix();
        }
        else
        {
            ToggleObject();
        }
    }

    public void ToggleObject()
    {
        if (FixingParticlesObject != null)
        {
            FixingParticlesObject.SetActive(false);
        }
        if (FixingTimeImage != null)
        {
            FixingTimeImage.gameObject.SetActive(false);
        }

        isFixing = false;
        IsFixed = !IsFixed;
        IsBugged = IsFixed;
        HandleToggle();
        GameManager.Instance.HandleBugToggleFix(this);
    }

    public abstract void HandleToggle();
    public abstract void HandleStartFix();
    public abstract void HandleStartBugging();
}