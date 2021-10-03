using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSeperateObjectsBugIncorrect : MonoBehaviour, IHighlightable
{
    public TwoSeperateObjectsBug parent;
    public TwoSeperateObjectsBugCorrect CorrectObject;

    Renderer Renderer;


    Color originalColor;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }
    public Color highlightColor => Constants.HIGHLIGHT_COLOR;


    // Start is called before the first frame update
    void Start()
    {
        if (parent == null)
        {
            parent = GetComponentInParent<TwoSeperateObjectsBug>();
        }
        if(Renderer == null)
        {
            Renderer = GetComponent<Renderer>();
        }
        CorrectObject = parent.CorrectObject;
        OriginalColor = Renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartFixing()
    {
        ToggleHighlight(false);
        StartCoroutine(MoveOverSeconds(parent.CorrectObject, parent.MaxFixTime));
    }

    public IEnumerator MoveOverSeconds(TwoSeperateObjectsBugCorrect destinationObject, float seconds)
    {
        float elapsedTime = 0;

        Vector3 startPosition = gameObject.transform.position;
        Vector3 endPosition = destinationObject.transform.position;

        Vector3 startScale = gameObject.transform.localScale;
        Vector3 endScale = destinationObject.transform.localScale;

        Quaternion startRotation = gameObject.transform.rotation;
        Quaternion endRotation = destinationObject.transform.rotation;

        while (elapsedTime < seconds)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / seconds));
            gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, (elapsedTime / seconds));
            gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endPosition;
        gameObject.transform.localScale = endScale;
        gameObject.transform.rotation = endRotation;
    }

    public void ToggleHighlight(bool highlighting)
    {
        if (highlighting && !parent.IsFixing)
        {
            Renderer.material.color = highlightColor;
        }
        else
        {
            Renderer.material.color = OriginalColor;
        }
    }
}
