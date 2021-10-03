using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureBug : Bug, IHighlightable
{
    [SerializeField] Renderer Renderer;

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }

    public Color highlightColor => Constants.HIGHLIGHT_COLOR;

    public override void DoStart()
    {
        Renderer.material.SetFloat("_Blend", 0);
    }

    public override void DoUpdate()
    {
    }

    public override void HandleAttemptBehaviour()
    {
    }

    public override void HandleStartBugging()
    {
    }

    public override void HandleStartFix()
    {
        ToggleHighlight(false);
        StartCoroutine(ChangeOverSeconds(MaxFixTime));
    }

    public override void HandleToggle()
    {
    }
    public IEnumerator ChangeOverSeconds(float seconds)
    {
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            Renderer.material.SetFloat("_Blend",Mathf.Lerp(0, 1, elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Renderer.material.SetFloat("_Blend", 1);
    }

    public void ToggleHighlight(bool highlighting)
    {
        if (!IsFixing && IsBugged && !IsFixed && highlighting)
        {
            Renderer.material.color = highlightColor;
        }
        else
        {
            Renderer.material.color = OriginalColor;
        }
    }
}
