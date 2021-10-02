using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStatePointoutable : MonoBehaviour
{
    public PointoutableCorrect Correct;
    public PointoutableIncorrect Incorrect;
    public float ScanTime;
    public bool isCorrect = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Correct == null)
        {
            Correct = GetComponentInChildren<PointoutableCorrect>();
        }
        if (Incorrect == null)
        {
            Incorrect = GetComponentInChildren<PointoutableIncorrect>();
        }
        SetObjectsActive();

        GameManager.Instance.RegisterBug(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleObject()
    {
        isCorrect = !isCorrect;
        SetObjectsActive();
        GameManager.Instance.HandleBugFixOrUnfix(this);
    }

    void SetObjectsActive()
    {
        Correct.gameObject.SetActive(isCorrect);
        Incorrect.gameObject.SetActive(!isCorrect);

    }
}
