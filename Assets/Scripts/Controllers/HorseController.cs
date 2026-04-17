using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class HorseController : MonoBehaviour
{
    [Header("Stats")]
    public HorseData horseData;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;

    [Header("Detection")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        // Freeze rotations except Y to prevent tipping over
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        CheckGround();
        ApplyRotation();
        UpdateAnimator();

        // Fallback/Direct input for jumping to ensure responsiveness
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }

    public void OnJump(InputValue value)
    {
        Jump();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (animator != null) animator.SetTrigger("Jump");
            Debug.Log("Horse Jumped!");
        }
    }

    private void CheckGround()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        float rayDistance = groundCheckDistance + 0.1f;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, rayDistance, groundLayer);
        
        // Debug visual in Scene View
        Debug.DrawRay(rayStart, Vector3.down * rayDistance, isGrounded ? Color.green : Color.red);
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveInput.y * moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    private void ApplyRotation()
    {
        if (moveInput.x != 0)
        {
            float rotation = moveInput.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }
    }

    private void UpdateAnimator()
    {
        if (animator == null) return;
        
        float currentSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("IsGrounded", isGrounded);
    }
}