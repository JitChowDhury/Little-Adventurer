using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 movementVelocity;
    private PlayerInput playerInput;
    private float verticalVelocity;
    private Animator animator;
    private float gravity = -9.8f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    private void CalculatePlayerMovement()
    {
        movementVelocity.Set(playerInput.horizontalInput, 0f, playerInput.verticalInput);
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(9, -45f, 0) * movementVelocity;

        animator.SetFloat("Speed", movementVelocity.magnitude);

        movementVelocity *= moveSpeed * Time.deltaTime;
        if (movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement();

        if (!characterController.isGrounded)
        {
            verticalVelocity = gravity;
        }
        else
        {
            verticalVelocity = gravity * 0.3f;
        }

        movementVelocity += verticalVelocity * Vector3.up * Time.deltaTime;

        characterController.Move(movementVelocity);

    }
}
