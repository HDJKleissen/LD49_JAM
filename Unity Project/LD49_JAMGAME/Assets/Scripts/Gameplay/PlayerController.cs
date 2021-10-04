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
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] float maxScanTime;
    [SerializeField] float interactRange;
    IFixable scanningBug = null;
    bool scanningNonBug = false;
    IHighlightable highlightedObject = null;
    Renderer highlightedNonBugNonInteractable = null;
    Color nonBugNonInteractableDefaultColor = Color.white;
    float scanTime = 0;

    Dictionary<GameObject, Renderer> objectsAndRenderers = new Dictionary<GameObject, Renderer>();

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        if (!GameManager.Instance.IsPaused && !GameManager.Instance.IsEnding)
        {
            UpdateMouseLook();
            UpdateMovement();
            HandleJumping();
            HandlePointing();

            previousGrounded = CharacterController.isGrounded;
        }
    }

    private void HandlePointing()
    {
        bool clearHighlight = true;
        RaycastHit hitInteractable, hitBug, hitOther;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bool rayHitInteractable = Physics.Raycast(ray, out hitInteractable, interactRange, interactableLayer);
        bool rayHitBug = Physics.Raycast(ray, out hitBug, interactRange, pointableLayer);
        bool rayHitOther = Physics.Raycast(ray, out hitOther, interactRange);
        if (rayHitBug)
        {
            IHighlightable highlightAbleObject = hitBug.transform.GetComponent<IHighlightable>();
            if(highlightAbleObject == null)
            {
                highlightAbleObject = hitBug.transform.GetComponentInParent<IHighlightable>();
            }
            if (highlightAbleObject != null)
            {
                clearHighlight = false;

                if (highlightedObject == null || highlightedObject != highlightAbleObject)
                {
                    StopHighlight();
                    highlightedObject?.ToggleHighlight(false);
                    highlightedObject = highlightAbleObject;
                    highlightedObject.ToggleHighlight(true);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                IFixable bugHit = hitBug.transform.GetComponent<IFixable>();

                if (bugHit == null)
                {
                    bugHit = hitBug.transform.GetComponentInParent<IFixable>();
                }

                if (bugHit != null)
                {
                    if ((scanningBug == null || scanningBug != bugHit) && !bugHit.IsFixing && bugHit.IsBugged && !bugHit.IsFixed)
                    {
                        StartScan(bugHit);
                    }
                }
                else
                {
                    Debug.LogWarning($"{hitBug.transform.name} is on the Pointoutables layer but does not have a Bug component and neither does its parent");
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if(scanningBug != null)
                {
                    scanTime += Time.deltaTime;
                }
            }

            // Temporary Triggering stuff
            if (Input.GetKeyDown(KeyCode.T))
            {
                Bug bugHit = hitBug.transform.GetComponent<Bug>();

                if (bugHit == null)
                {
                    bugHit = hitBug.transform.GetComponentInParent<Bug>();
                }

                if (bugHit != null)
                {
                    if (!bugHit.IsBugged)
                    {
                        bugHit.StartBugging();
                    }
                }
            }
        }
        else if(!scanningNonBug && scanningBug != null)
        {
            StopScan();
        }

        if (rayHitOther && !rayHitBug && !rayHitInteractable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartScan();
            }
            else if (scanningBug != null)
            {
                StopScan();
            }
            else if(Input.GetMouseButton(0))
            {
                if (scanningNonBug)
                {
                    scanTime += Time.deltaTime;
                }
            }
        }
        else if (scanningNonBug)
        {
            StopScan();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopScan();
        }

        if (rayHitInteractable)
        {
            Interactable interactableHit = hitInteractable.transform.GetComponent<Interactable>();

            if (interactableHit != null)
            {
                clearHighlight = false;

                if (highlightedObject == null || highlightedObject != interactableHit as IHighlightable)
                {
                    StopHighlight();
                    highlightedObject?.ToggleHighlight(false);
                    highlightedObject = interactableHit;
                    highlightedObject.ToggleHighlight(true);
                }
            }
            
            if (Input.GetButtonDown("Use"))
            {
                interactableHit.Interact();

            }
        }
        if (rayHitOther && !rayHitInteractable && !rayHitBug)
        {
            GameObject hitObject = hitOther.transform.gameObject;
            if (hitObject.tag != "Player")
            {
                if (hitObject.tag == "NPC")
                {

                }
                else
                {
                    Renderer renderer;

                    if (objectsAndRenderers.ContainsKey(hitObject))
                    {
                        renderer = objectsAndRenderers[hitObject];
                    }
                    else
                    {
                        renderer = hitObject.GetComponent<Renderer>();
                        objectsAndRenderers.Add(hitObject, renderer);
                    }
                    if (highlightedNonBugNonInteractable != null && highlightedNonBugNonInteractable != renderer)
                    {
                        highlightedNonBugNonInteractable.material.color = nonBugNonInteractableDefaultColor;
                    }

                    StopHighlight();
                    renderer.material.color = Constants.HIGHLIGHT_COLOR;
                    highlightedNonBugNonInteractable = renderer;
                    clearHighlight = false;
                }
            }
        }

        if (clearHighlight && (highlightedObject != null || highlightedNonBugNonInteractable != null))
        {
            StopHighlight();
        }

        if (scanningBug != null || scanningNonBug)
        {
            GameManager.Instance.UpdateScanningUI(scanTime, maxScanTime);
            if (scanTime >= maxScanTime)
            {
                if (scanningNonBug)
                {
                    GameManager.Instance.BugReportFailure(); 
                }
                else
                {
                    scanningBug.StartFix();
                }
                StopScan();
            }
        }
    }

    private void StopHighlight()
    {
        if (highlightedNonBugNonInteractable != null)
        {
            Debug.Log("Clearing highnonbugnonint");
            highlightedNonBugNonInteractable.material.color = nonBugNonInteractableDefaultColor;
        }
        highlightedObject?.ToggleHighlight(false);
        highlightedObject = null;
        highlightedNonBugNonInteractable = null;
        nonBugNonInteractableDefaultColor = Color.white;
    }

    void StartScan()
    {
        StopScan();
        Debug.Log("Starting wrong scan");
        scanningNonBug = true;
        scanTime = 0;
        maxScanTime = 1;
    }

    void StartScan(IFixable bug)
    {
        StopScan();
        Debug.Log("Starting scan");
        scanningBug = bug;
        scanTime = 0;
        maxScanTime = 1;
    }

    void StopScan()
    {
        StopHighlight();
        scanningNonBug = false;
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

        float moveSpeedWithSprinting = moveSpeed;
        if (Input.GetButton("Sprint"))
        {
            moveSpeedWithSprinting *= 2;
        }

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeedWithSprinting + Vector3.up * velocityY;

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