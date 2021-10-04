using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugShowCamera : MonoBehaviour
{
    public EndingController EndingController;
    public Camera Camera;
    public float MoveSpeed;
    public Renderer ColoredPlane;
    public Bug Bug;


    bool updatedCounter = false;
    bool rotateRight;
    Vector3 originalScale;
    public Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;

        Camera.transform.position = Bug.FixingParticlesLocation.position + new Vector3(3, 1.25f, 0);
        rotateRight = Random.Range(0, 1f) > 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x <= Screen.width / 2 - screenPosition.y % (Screen.height / 3) + (Screen.height / 3))
        {
            if (!updatedCounter)
            {
                if (Bug.IsFixed)
                {
                    // Play Ding! sound
                    EndingController.EndScreenUI.AddFixedBug();
                }
                else
                {
                    // Play ERR sound
                    EndingController.EndScreenUI.AddUnfixedBug();
                }
                EndingController.EndScreenUI.UpdateGrade();
                updatedCounter = true;
                transform.localScale = originalScale * 1.1f;
                transform.position = transform.position - new Vector3(0, 0, 0.5f);
                ColoredPlane.gameObject.SetActive(true);
                ColoredPlane.material.color = Bug.IsFixed ? Color.green : Color.red;
                StartCoroutine(MoveOverSeconds(transform.position.z + 0.5f, originalScale, 0.2f));
            }
        }
        if(screenPosition.x < 0 - Screen.width/3)
        {
            Destroy(Camera.gameObject);
            Destroy(gameObject);
            EndingController.PanelWasDestroyed();
        }

        // Camera thing

        Camera.transform.LookAt(Bug.FixingParticlesLocation);
        if (rotateRight)
        {
            Camera.transform.Translate(Vector3.right * 1.2f * Time.deltaTime);
        }
        else
        {
            Camera.transform.Translate(Vector3.left * 1.2f * Time.deltaTime);
        }
    }

    public IEnumerator MoveOverSeconds(float destinationZ, Vector3 destinationScale, float seconds)
    {
        float elapsedTime = 0;

        float startZ = transform.position.z;
        float endZ = destinationZ;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = destinationScale;

        while (elapsedTime < seconds)
        {
            float newZ = Mathf.Lerp(startZ, endZ, elapsedTime / seconds);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, endZ);
        transform.localScale = endScale;
    }

}
