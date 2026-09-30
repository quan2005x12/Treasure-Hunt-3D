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
    public float chestInteractionDistance = 3f;
    public float chestAnimationDuration = 1.5f;

    private CharacterController controller;

    private float verticalVelocity;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private bool isOpeningChest = false;

    private Chest nearbyChest = null;

    private Vector3 startPosition;
    private Quaternion startRotation;

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

        startPosition =
            transform.position;

        startRotation =
            transform.rotation;
    }

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        CheckMapBoundary();

        if (isOpeningChest)
        {
            HandleGravity();
            return;
        }

        UpdateGroundedState();
        HandleMovement();
        HandleJump();
        HandleChestDetection();
        HandleInteraction();
        UpdateAnimation();
    }

    void CheckMapBoundary()
    {
        if (transform.position.y <
            startPosition.y - 15f)
        {
            ResetPlayerPosition();
        }
    }

    void ResetPlayerPosition()
    {
        controller.enabled = false;

        transform.position =
            startPosition;

        transform.rotation =
            startRotation;

        controller.enabled = true;

        verticalVelocity = 0f;
        coyoteTimeCounter = 0f;
        jumpBufferCounter = 0f;

        isOpeningChest = false;
        nearbyChest = null;

        if (anim != null)
        {
            anim.SetBool(
                "isMoving",
                false
            );

            anim.speed = 1f;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowMessage(
                "Bạn đã rời khỏi bản đồ!"
            );
        }

        Debug.Log(
            "Luffy đã trở về vị trí ban đầu."
        );
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

    void HandleChestDetection()
    {
        Collider[] colliders =
            Physics.OverlapSphere(
                transform.position,
                chestInteractionDistance
            );

        Chest closestChest = null;

        float closestDistance =
            chestInteractionDistance;

        foreach (Collider collider in colliders)
        {
            Chest chest =
                collider.GetComponent<Chest>();

            if (chest == null)
            {
                chest =
                    collider.GetComponentInParent<Chest>();
            }

            if (chest == null)
            {
                continue;
            }

            if (chest.IsOpen())
            {
                continue;
            }

            Vector3 closestPoint =
                collider.ClosestPoint(
                    transform.position
                );

            float distance =
                Vector3.Distance(
                    transform.position,
                    closestPoint
                );

            if (distance < closestDistance)
            {
                closestDistance =
                    distance;

                closestChest =
                    chest;
            }
        }

        nearbyChest =
            closestChest;

        if (nearbyChest == null)
        {
            return;
        }

        if (nearbyChest.requireAllKeys &&
            !nearbyChest.CanOpen())
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowMessage(
                    "Cần đủ 3 chìa khóa để mở rương."
                );
            }

            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowMessage(
                "Phát hiện rương! Nhấn E để mở."
            );
        }
    }

    void HandleInteraction()
    {
        if (!Keyboard.current.eKey.wasPressedThisFrame)
        {
            return;
        }

        if (nearbyChest == null)
        {
            return;
        }

        if (nearbyChest.IsOpen())
        {
            return;
        }

        if (!nearbyChest.CanOpen())
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowMessage(
                    "Bạn chưa có đủ 3 chìa khóa."
                );
            }

            return;
        }

        StartCoroutine(
            OpenChestRoutine(
                nearbyChest
            )
        );
    }

    IEnumerator OpenChestRoutine(
        Chest chest
    )
    {
        isOpeningChest = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.HideMessage();
        }

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

        nearbyChest = null;
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