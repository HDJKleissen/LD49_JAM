using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float coyoteTime;

    bool canJump;
    bool previousGrounded = false;
    float cameraPitch = 0.0f;
    float velocityY = 0.0f;

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime;


    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {

    }
    

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        if (canJump)
        {
            if (Input.GetButtonDown("Jump"))
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

        previousGrounded = CharacterController.isGrounded;
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
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

        canJump = true;
    }
}
