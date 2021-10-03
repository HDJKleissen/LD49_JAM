using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI BugCounterText;
    public Image ScanCircleImage;
    public FailureCounter failureCounter;
    public HintsOverlay hintsOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBugCounter(int currentAmount, int totalAmount)
    {
        BugCounterText.SetText($"{currentAmount}/{totalAmount}");
    }

    internal void UpdateScanningUI(float percentage)
    {
        if(!ScanCircleImage.gameObject.activeInHierarchy)
        {
            ScanCircleImage.gameObject.SetActive(true);
        }
        ScanCircleImage.fillAmount = percentage;
    }

    internal void DisableScanUI()
    {
        ScanCircleImage.fillAmount = 0;
        ScanCircleImage.gameObject.SetActive(false);
    }

    internal void InitMaxFailures(int amount)
    {
        failureCounter.InitMaxFailureAmount(amount);
    }

    internal void AddFailureImage()
    {
        failureCounter.AddFailure();
    }

    internal void ShowHint(string text, float showTime)
    {
        hintsOverlay.SetHint(text, showTime);
    }
}
