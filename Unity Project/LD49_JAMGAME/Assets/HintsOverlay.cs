using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintsOverlay : MonoBehaviour
{
    public TextMeshProUGUI HintsText;
    public GameObject HintsPanel;

    float MaxLineWidth;
    public float AverageCharacterWidth;

    // Start is called before the first frame update
    void Start()
    {
        MaxLineWidth = Screen.width * 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHint(string text, float showTime)
    {
        int textLength = text.Length;
        string[] words = text.Split(' ');

        float textWidth = AverageCharacterWidth * textLength;

        bool addedNewLine = false;

        float lengthCounter = 0;

        for(int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            float wordLength = word.Length * AverageCharacterWidth;
            lengthCounter += wordLength;
            if(lengthCounter > MaxLineWidth)
            {
                word = "\n" + word;
                lengthCounter = 0;
                addedNewLine = true;
            }
            words[i] = word;
        }

        string textWithNewlines = string.Join(" ", words);
        if (addedNewLine)
        {
            HintsText.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            HintsText.alignment = TextAlignmentOptions.Center;
        }
        HintsText.SetText(textWithNewlines);
        HintsPanel.SetActive(true);

        StartCoroutine(CoroutineHelper.DelaySeconds(() => HintsPanel.SetActive(false), showTime));
    }
}
