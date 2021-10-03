using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IHighlightable
{
    public Bug LinkedBug;
    public Renderer ObjectRenderer;
    Vector3 originalScale;

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor=value; }

    public Color highlightColor => Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = ObjectRenderer.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        LinkedBug.AttemptBehaviour();
    }

    public void ToggleHighlight(bool highlighting)
    {
        if (highlighting)
        {
            ObjectRenderer.material.color = highlightColor;
            ObjectRenderer.transform.localScale = originalScale * 1.2f;
        }
        else
        {
            ObjectRenderer.material.color = OriginalColor;
            ObjectRenderer.transform.localScale = originalScale;
        }
    }
}
