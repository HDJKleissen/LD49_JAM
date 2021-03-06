using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBug : Bug, IHighlightable
{
    [SerializeField] Renderer Renderer;
    [SerializeField] Color startColor, endColor;
    [SerializeField] Light Light;

    Color originalColor = Color.white;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }

    public Color highlightColor => Constants.HIGHLIGHT_COLOR;

    Texture originalEmissionMap;
    public override void DoStart()
    {
        if (Light == null)
        {
            Light = GetComponentInChildren<Light>();
        }
        if (Renderer == null)
        {
            Renderer = GetComponentInChildren<Renderer>();
        }
        Light.color = startColor;
        originalEmissionMap = Renderer.materials[0].GetTexture("_EmissionMap");
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
            Light.color = Color.Lerp(startColor, endColor, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Light.color = endColor;
    }

    public void ToggleHighlight(bool highlighting)
    {
        if (highlighting && !IsFixing)
        {
            if (tag == "HasEmissionMap")
            {
                Renderer.materials[0].SetTexture("_EmissionMap", null);
            }
            else
            {
                Renderer.materials[0].EnableKeyword("_EMISSION");
            }
            Renderer.materials[0].SetColor("_EmissionColor", highlightColor);
        }
        else
        {
            if (tag == "HasEmissionMap")
            {
                Renderer.materials[0].SetTexture("_EmissionMap", originalEmissionMap);
            }
            else
            {
                Renderer.materials[0].DisableKeyword("_EMISSION");
            }
            Renderer.materials[0].SetColor("_EmissionColor", OriginalColor);
        }
    }
}
