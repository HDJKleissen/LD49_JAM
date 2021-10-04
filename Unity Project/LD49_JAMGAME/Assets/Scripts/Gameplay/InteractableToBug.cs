using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableToBug : MonoBehaviour, IHighlightable
{
    public Bug LinkedBug;
    public Renderer ObjectRenderer;

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }

    public Color highlightColor => Constants.INTERACTABLE_COLOR;

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
            foreach(Material mat in ObjectRenderer.materials)
            {
                mat.SetColor("_EmissionColor", highlightColor);
                mat.EnableKeyword("_EMISSION");
            }
        }
        else
        {
            foreach (Material mat in ObjectRenderer.materials)
            {
                ObjectRenderer.materials[0].DisableKeyword("_EMISSION");
            }
        }
    }
}