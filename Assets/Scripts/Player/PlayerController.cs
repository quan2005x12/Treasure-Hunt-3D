using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f;
    public float runSpeed = 7f;
    public float rotationSpeed = 10f;

    [Header("Jump")]
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Animation")]
    public Animator anim;
    public float runAnimationSpeed = 1.5f;

    [Header("Chest")]
    public float chestInteractionDistance = 2f;
    public float chestAnimationDuration = 1.5f;

    private CharacterController controller;

    private float verticalVelocity;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private bool isOpeningChest = false;

    void Start()
    {
        controller =
            GetComponent<CharacterController>();

        if (anim == null)
        {
            anim =
                GetComponent<Animator>();
        }

        if (cameraTransform == null)
        {
            Camera mainCamera =
                Camera.main;

            if (mainCamera != null)
            {
                cameraTransform =
                    mainCamera.transform;
            }
        }
    }

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (isOpeningChest)
        {
            HandleGravity();
            return;
        }

        UpdateGroundedState();
        HandleMovement();
        HandleJump();
        HandleInteraction();
        UpdateAnimation();
    }

    void UpdateGroundedState()
    {
        if (controller.isGrounded)
        {
            coyoteTimeCounter =
                coyoteTime;
        }
        else
        {
            coyoteTimeCounter -=
                Time.deltaTime;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpBufferCounter =
                jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -=
                Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Chỉ dùng phím mũi tên để di chuyển
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1f;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1f;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1f;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1f;
        }

        Vector3 input =
            new Vector3(
                horizontal,
                0f,
                vertical
            ).normalized;

        if (input.sqrMagnitude < 0.01f)
        {
            return;
        }

        Vector3 moveDirection;

        if (cameraTransform != null)
        {
            Vector3 cameraForward =
                cameraTransform.forward;

            Vector3 cameraRight =
                cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDirection =
                cameraForward * input.z +
                cameraRight * input.x;
        }
        else
        {
            moveDirection =
                input;
        }

        moveDirection.Normalize();

        bool isRunning =
            Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed;

        float currentSpeed =
            isRunning
                ? runSpeed
                : speed;

        controller.Move(
            moveDirection *
            currentSpeed *
            Time.deltaTime
        );

        Quaternion targetRotation =
            Quaternion.LookRotation(
                moveDirection
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed *
                Time.deltaTime
            );
    }

    void HandleJump()
    {
        if (controller.isGrounded &&
            verticalVelocity < 0f)
        {
            verticalVelocity =
                -2f;
        }

        bool canJump =
            coyoteTimeCounter > 0f &&
            jumpBufferCounter > 0f;

        if (canJump)
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight *
                    -2f *
                    gravity
                );

            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;

            if (anim != null)
            {
                anim.SetTrigger(
                    "Jump"
                );
            }
        }

        verticalVelocity +=
            gravity *
            Time.deltaTime;

        controller.Move(
            Vector3.up *
            verticalVelocity *
            Time.deltaTime
        );
    }

    void HandleGravity()
    {
        if (controller.isGrounded &&
            verticalVelocity < 0f)
        {
            verticalVelocity =
                -2f;
        }

        verticalVelocity +=
            gravity *
            Time.deltaTime;

        controller.Move(
            Vector3.up *
            verticalVelocity *
            Time.deltaTime
        );
    }

    void HandleInteraction()
    {
        if (!Keyboard.current.eKey.wasPressedThisFrame)
        {
            return;
        }

        Chest[] chests =
            FindObjectsByType<Chest>(
                FindObjectsSortMode.None
            );

        Chest nearestChest = null;

        float nearestDistance =
            chestInteractionDistance;

        foreach (Chest chest in chests)
        {
            if (chest.IsOpen())
            {
                continue;
            }

            Collider chestCollider =
                chest.GetComponent<Collider>();

            float distance;

            if (chestCollider != null)
            {
                Vector3 closestPoint =
                    chestCollider.ClosestPoint(
                        transform.position
                    );

                distance =
                    Vector3.Distance(
                        transform.position,
                        closestPoint
                    );
            }
            else
            {
                distance =
                    Vector3.Distance(
                        transform.position,
                        chest.transform.position
                    );
            }

            if (distance <= nearestDistance)
            {
                nearestDistance =
                    distance;

                nearestChest =
                    chest;
            }
        }

        if (nearestChest != null)
        {
            StartCoroutine(
                OpenChestRoutine(
                    nearestChest
                )
            );
        }
    }

    IEnumerator OpenChestRoutine(
        Chest chest
    )
    {
        isOpeningChest = true;

        Vector3 direction =
            chest.transform.position -
            transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(
                    direction
                );

            transform.rotation =
                targetRotation;
        }

        if (anim != null)
        {
            anim.SetBool(
                "isMoving",
                false
            );

            anim.SetTrigger(
                "OpenChest"
            );

            anim.speed = 1f;
        }

        yield return new WaitForSeconds(
            chestAnimationDuration
        );

        chest.OpenChest();

        isOpeningChest = false;
    }

    void UpdateAnimation()
    {
        if (anim == null)
        {
            return;
        }

        bool isMoving =
            Keyboard.current.upArrowKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed;

        bool isRunning =
            Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed;

        anim.SetBool(
            "isMoving",
            isMoving
        );

        if (controller.isGrounded &&
            isMoving &&
            isRunning)
        {
            anim.speed =
                runAnimationSpeed;
        }
        else
        {
            anim.speed = 1f;
        }
    }
}