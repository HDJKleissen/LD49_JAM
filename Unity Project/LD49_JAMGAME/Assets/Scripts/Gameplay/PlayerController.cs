using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;

    // Vertical Physics
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float coyoteTime;

    bool canJump;
    bool previousGrounded = false;
    float velocityY = 0.0f;

    // Camera & Movement
    [SerializeField] Camera playerCamera = null;
    [SerializeField] float moveSpeed;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime;
    float cameraPitch = 0.0f;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Pointing out bugs
    [SerializeField] LayerMask pointableLayer;
    [SerializeField] float maxScanTime;

    TwoSeperateObjectsBug scanningBug = null;
    float scanTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        HandleJumping();
        HandlePointing();


        previousGrounded = CharacterController.isGrounded;
    }

    private void HandlePointing()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.MaxValue, ~1 << pointableLayer))
            {
                TwoSeperateObjectsBugIncorrect objectHit = hit.transform.GetComponent<TwoSeperateObjectsBugIncorrect>();

                if (objectHit != null)
                {
                    if (objectHit.parent == null)
                    {
                        Debug.LogWarning($"Object {objectHit.name} does not have a parent!");
                    }
                    if ((scanningBug == null || scanningBug != objectHit.parent) && !objectHit.parent.isFixing)
                    {
                        StartScan(objectHit.parent);
                    }
                    else if (scanningBug == objectHit.parent)
                    {
                        scanTime += Time.deltaTime;
                    }
                }
                else
                {
                    if (scanningBug != null)
                    {
                        StopScan();
                    }
                }
            }
            else
            {
                if (scanningBug != null)
                {
                    StopScan();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopScan();
        }

        if (scanningBug != null)
        {
            GameManager.Instance.UpdateScanningUI(scanTime, maxScanTime);
            if (scanTime >= maxScanTime)
            {
                scanningBug.ToggleObject();
                StopScan();
            }
        }
    }



    void StartScan(TwoSeperateObjectsBug bug)
    {
        scanningBug = bug;
        scanTime = 0;
        maxScanTime = scanningBug.ScanTime;
    }

    void StopScan()
    {
        scanningBug = null;
        scanTime = 0;
        maxScanTime = -1;
        GameManager.Instance.DisableScanUI();
    }

    private void HandleJumping()
    {
        if (canJump)
        {
            if (Input.GetButton("Jump"))
            {
                canJump = false;
                StartCoroutine(JumpEvent());
            }
        }
        if (CharacterController.isGrounded && !canJump)
        {
            canJump = true;
        }
        if (!CharacterController.isGrounded && previousGrounded)
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => canJump = false, coyoteTime));
        }
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * GameManager.MouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * GameManager.MouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (CharacterController.isGrounded)
            velocityY = -0.1f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;

        CharacterController.Move(velocity * Time.deltaTime);
    }
    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            CharacterController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!CharacterController.isGrounded && CharacterController.collisionFlags != CollisionFlags.Above);
    }
}