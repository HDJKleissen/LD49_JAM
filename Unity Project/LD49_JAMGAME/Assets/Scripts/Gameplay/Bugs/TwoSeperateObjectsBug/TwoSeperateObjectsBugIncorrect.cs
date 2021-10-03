using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSeperateObjectsBugIncorrect : MonoBehaviour, IHighlightable
{
    public TwoSeperateObjectsBug parent;
    public TwoSeperateObjectsBugCorrect CorrectObject;
    public bool GlitchyMovement = false, GlitchyRotation;
    [SerializeField] float BaseGlitchMoveAmplitude = 0.05f;
    [SerializeField] float BaseGlitchRotateAmplitude = 1f;
    Vector3 baseLocation, baseRotationEulers;
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
        baseLocation = transform.localPosition;
        baseRotationEulers = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlitchyMovement)
        {
            transform.localPosition = baseLocation + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * BaseGlitchMoveAmplitude;
        }
        if (GlitchyRotation)
        {
            transform.localRotation = Quaternion.Euler(baseRotationEulers + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * BaseGlitchRotateAmplitude);
        }
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

        float startGlitchMoveAmplitude = BaseGlitchMoveAmplitude;
        float endGlitchMoveAmplitude = 0;
        float startGlitchRotateAmplitude = BaseGlitchRotateAmplitude;
        float endGlitchRotateAmplitude = 0;

        while (elapsedTime < seconds)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / seconds);
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / seconds);
            if (GlitchyMovement)
            {
                newPosition += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * BaseGlitchMoveAmplitude;
            }
            if (GlitchyRotation)
            {
                newRotation = Quaternion.Euler(newRotation.eulerAngles + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * BaseGlitchRotateAmplitude);
            }
            gameObject.transform.position = newPosition;
            gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / seconds);
            gameObject.transform.rotation = newRotation;
            BaseGlitchMoveAmplitude = Mathf.Lerp(startGlitchMoveAmplitude, endGlitchMoveAmplitude, elapsedTime / seconds);
            BaseGlitchRotateAmplitude = Mathf.Lerp(startGlitchRotateAmplitude, endGlitchRotateAmplitude, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endPosition;
        gameObject.transform.localScale = endScale;
        gameObject.transform.rotation = endRotation;
        BaseGlitchMoveAmplitude = endGlitchMoveAmplitude;
        BaseGlitchRotateAmplitude = endGlitchRotateAmplitude;
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
