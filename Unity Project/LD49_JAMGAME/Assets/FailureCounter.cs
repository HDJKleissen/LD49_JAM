using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureCounter : MonoBehaviour
{
    [SerializeField] Transform ImageParent;
    [SerializeField] GameObject FailureIconPrefab;
    int failureAmount = 0;
    List<Image> FailureIcons = new List<Image>();

    [SerializeField] Sprite FailureIcon, FailureIconFilled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitMaxFailureAmount(int maxFailures)
    {
        for(int i = 0; i < maxFailures; i++)
        {
            FailureIcons.Add(Instantiate(FailureIconPrefab, ImageParent).GetComponent<Image>());
        }
    }

    public void AddFailure()
    {
        failureAmount++;
        if (failureAmount <= FailureIcons.Count)
        {
            FailureIcons[FailureIcons.Count - failureAmount].sprite = FailureIconFilled;
        }
    }
}
