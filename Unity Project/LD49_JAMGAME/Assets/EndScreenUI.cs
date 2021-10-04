using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    public TextMeshProUGUI FixedBugsText, UnfixedBugsText, GradeText;

    int fixedBugs = 0, unfixedBugs = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFinalButtons()
    {

    }

    public void AddFixedBug()
    {
        fixedBugs++;
        FixedBugsText.SetText(fixedBugs.ToString());
    }
    public void AddUnfixedBug()
    {
        unfixedBugs++;
        UnfixedBugsText.SetText(unfixedBugs.ToString());
    }

    public void UpdateGrade()
    {
        int totalBugs = fixedBugs + unfixedBugs;
        int maxBugs = GameManager.Instance.bugsInLevel.Count - 1; // -1 because of double elevator

        switch (fixedBugs - unfixedBugs)
        {
            case var _ when fixedBugs - unfixedBugs > 4 * ((float)maxBugs / 5):
                SetGradeText("S");
                break;
            case var _ when fixedBugs - unfixedBugs > 3 * ((float)maxBugs / 5):
                SetGradeText("A");
                break;
            case var _ when fixedBugs - unfixedBugs > 2 * ((float)maxBugs / 5):
                SetGradeText("B");
                break;
            case var _ when fixedBugs - unfixedBugs > ((float)maxBugs / 5):
                SetGradeText("C");
                break;
            case var _ when fixedBugs - unfixedBugs >= 0:
                SetGradeText("D");
                break;
            case var _ when fixedBugs - unfixedBugs < 0:
                SetGradeText("F");
                break;
        }

        if (totalBugs == maxBugs)
        {
            if (fixedBugs == maxBugs)
            {
                SetGradeText("S+!");
            }
        }
        
    }

    void SetGradeText(string grade)
    {
        GradeText.SetText("Grade: " + grade);
    }
}
