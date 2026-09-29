using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Animator animator;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector3 movement;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();
        animator.SetFloat("Speed", movement.magnitude);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void ReadInput()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.wKey.isPressed)
            moveInput.y += 1f;

        if (Keyboard.current.sKey.isPressed)
            moveInput.y -= 1f;

        if (Keyboard.current.dKey.isPressed)
            moveInput.x += 1f;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x -= 1f;

        moveInput = Vector2.ClampMagnitude(moveInput, 1f);

        movement = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );
    }

    private void Move()
    {
        Vector3 targetPosition =
            rb.position +
            movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);
    }

    private void Rotate()
    {
        if (movement.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(movement);

        Quaternion smoothRotation =
            Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

        rb.MoveRotation(smoothRotation);
    }
}