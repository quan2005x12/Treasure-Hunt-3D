using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Position")]
    public float distance = 10f;
    public float height = 2.5f;
    public float lookHeight = 1.2f;

    [Header("Camera Rotation")]
    public float rotationSpeed = 60f;
    public float minVerticalAngle = 0f;
    public float maxVerticalAngle = 55f;

    [Header("Follow")]
    public float followSpeed = 10f;

    [Header("Collision")]
    public bool useCollision = true;
    public LayerMask collisionMask = ~0;
    public float collisionRadius = 0.3f;
    public float minDistance = 1.5f;
    public float collisionSmoothSpeed = 15f;

    private float horizontalAngle;
    private float verticalAngle = 18f;

    private float currentDistance;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning(
                "CameraFollow: Chưa gán Target."
            );

            return;
        }

        currentDistance = distance;

        // Luffy.forward là hướng phía trước.
        // Vector3.back của góc camera sẽ đưa camera
        // về phía sau Luffy.
        horizontalAngle =
            target.eulerAngles.y;

        verticalAngle = 18f;

        SetCameraImmediately();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        HandleCameraInput();
        HandleCameraPosition();
    }

    void HandleCameraInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        // A / D: xoay camera trái / phải
        if (Keyboard.current.aKey.isPressed)
        {
            horizontalAngle -=
                rotationSpeed *
                Time.deltaTime;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            horizontalAngle +=
                rotationSpeed *
                Time.deltaTime;
        }

        // W / S: nhìn lên / xuống
        if (Keyboard.current.wKey.isPressed)
        {
            verticalAngle +=
                rotationSpeed *
                Time.deltaTime;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            verticalAngle -=
                rotationSpeed *
                Time.deltaTime;
        }

        verticalAngle =
            Mathf.Clamp(
                verticalAngle,
                minVerticalAngle,
                maxVerticalAngle
            );
    }

    void HandleCameraPosition()
    {
        Vector3 focusPosition =
            target.position +
            Vector3.up *
            lookHeight;

        Quaternion rotation =
            Quaternion.Euler(
                verticalAngle,
                horizontalAngle,
                0f
            );

        Vector3 direction =
            rotation *
            Vector3.back;

        float targetDistance =
            distance;

        if (useCollision)
        {
            if (Physics.SphereCast(
                focusPosition,
                collisionRadius,
                direction,
                out RaycastHit hit,
                distance,
                collisionMask,
                QueryTriggerInteraction.Ignore))
            {
                if (hit.transform != target &&
                    !hit.transform.IsChildOf(target))
                {
                    targetDistance =
                        Mathf.Clamp(
                            hit.distance -
                            collisionRadius,
                            minDistance,
                            distance
                        );
                }
            }
        }

        currentDistance =
            Mathf.Lerp(
                currentDistance,
                targetDistance,
                collisionSmoothSpeed *
                Time.deltaTime
            );

        Vector3 desiredPosition =
            focusPosition +
            direction.normalized *
            currentDistance;

        transform.position =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                followSpeed *
                Time.deltaTime
            );

        transform.LookAt(
            focusPosition
        );
    }

    void SetCameraImmediately()
    {
        Vector3 focusPosition =
            target.position +
            Vector3.up *
            lookHeight;

        Quaternion rotation =
            Quaternion.Euler(
                verticalAngle,
                horizontalAngle,
                0f
            );

        Vector3 direction =
            rotation *
            Vector3.back;

        transform.position =
            focusPosition +
            direction *
            distance;

        transform.LookAt(
            focusPosition
        );
    }
}