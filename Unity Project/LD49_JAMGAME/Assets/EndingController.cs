using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    public GameObject PanelPrefab;
    public GameObject Background;
    public GameObject EndButtonsPanel;
    public Camera MainCamera;
    public float CameraDistanceFromBackground;
    public EndScreenUI EndScreenUI;
    Vector3 topLeft, topRight, bottomLeft, bottomRight;
    Vector3 panelSize;

    int panelsCreated = 0;
    int panelsDestroyed = 0;

    Dictionary<Camera, RenderTexture> cameras = new Dictionary<Camera, RenderTexture>();
    List<Bug> Bugs = new List<Bug>();

    public void StartEnding()
    {
        EndScreenUI.gameObject.SetActive(true);
        Bugs = new List<Bug>(GameManager.Instance.bugsInLevel);
        MainCamera.transform.position = Background.transform.position - Background.transform.forward * CameraDistanceFromBackground;
        MainCamera.transform.LookAt(Background.transform);
        Background.transform.position += Background.transform.forward;

        Ray bottomLeftRay = MainCamera.ViewportPointToRay(new Vector3(0, 0, 0));
        Ray topLeftRay = MainCamera.ViewportPointToRay(new Vector3(0, 1, 0));
        Ray bottomRightRay = MainCamera.ViewportPointToRay(new Vector3(1, 0, 0));
        Ray topRightRay = MainCamera.ViewportPointToRay(new Vector3(1, 1, 0));

        RaycastHit hit;
        if (Physics.Raycast(bottomLeftRay, out hit))
        {
            bottomLeft = hit.point;
        }
        else
        {
            Debug.Log("something is fucky");
        }
        if (Physics.Raycast(topLeftRay, out hit))
        {
            topLeft = hit.point;
        }
        else
        {
            Debug.Log("something is fucky");
        }
        if (Physics.Raycast(bottomRightRay, out hit))
        {
            bottomRight = hit.point;
        }
        else
        {
            Debug.Log("something is fucky");
        }
        if (Physics.Raycast(topRightRay, out hit))
        {
            topRight = hit.point;
        }
        else
        {
            Debug.Log("something is fucky");
        }

        MainCamera.transform.position -= Background.transform.forward;

        panelSize = (new Vector3((topRight.x - topLeft.x), 0.1f, (topLeft.y - bottomLeft.y)) * 0.95f) / 10;
        StartCoroutine(SpawnStuff());
    }

    public void PanelWasDestroyed()
    {
        panelsDestroyed++;

        if(panelsDestroyed >= panelsCreated)
        {
            EndButtonsPanel.SetActive(true);
        }
    }

    IEnumerator SpawnStuff()
    {
        List<int> allowedYValues = new List<int> { 1 };
        int BugsAmount = Bugs.Count;
        for (int i = 0; i < BugsAmount; i++)
        {
            Camera newCamera = new GameObject().AddComponent<Camera>();
            newCamera.gameObject.transform.localScale = new Vector3(1, -1, 1);
            RenderTexture newTexture = new RenderTexture(800, 450, 16, RenderTextureFormat.ARGB32);

            GameObject target = Instantiate(PanelPrefab);

            // Prepare yourself

            int usedYValue = allowedYValues[i % allowedYValues.Count]; 

            Vector3 targetPosition = topRight + new Vector3(panelSize.x * 10,0,0) + ((bottomLeft - topLeft) / 8f) + ((bottomLeft - topLeft) / 3) * usedYValue;
            target.transform.position = targetPosition;
            target.transform.localScale = panelSize;

            Bug pickedBug = Bugs[Random.Range(0, Bugs.Count)];
            Bugs.Remove(pickedBug);

            BugShowCamera moveScript = target.GetComponent<BugShowCamera>();
            moveScript.Camera = newCamera;
            moveScript.EndingController = this;
            moveScript.MoveSpeed = 9 * ((float)BugsAmount / 10);
            moveScript.Bug = pickedBug;
            //moveScript.startPosition = topLeft + baseOffset + ((bottomLeft - topLeft) / (CameraAmountVertical)) * usedYValue;
            Renderer targetRenderer = target.GetComponent<Renderer>();
            targetRenderer.material.mainTexture = newTexture;


            newCamera.name = "EndCamera " + i;
            newCamera.targetTexture = newTexture;
            cameras.Add(newCamera, newTexture);
            panelsCreated++;
            yield return new WaitForSeconds(2.25f / ((float)BugsAmount / 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}