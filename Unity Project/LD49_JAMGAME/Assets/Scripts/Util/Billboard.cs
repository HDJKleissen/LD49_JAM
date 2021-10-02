using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        transform.forward = mainCamera.transform.forward;
    }
}