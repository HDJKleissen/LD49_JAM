using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IHighlightable
{
    public Bug LinkedBug;
    public Renderer ObjectRenderer;

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }

    public Color highlightColor => Constants.HIGHLIGHT_COLOR;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            ObjectRenderer.material.color = OriginalColor;
        }
    }
}