using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI BugCounterText;

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
}
