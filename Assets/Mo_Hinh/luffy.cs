using UnityEngine;
using UnityEngine.InputSystem;

public class luffy : MonoBehaviour
{
    public float speed = 4f;
    public float openChestDuration = 1.5f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;      // Lực nhảy ban đầu
    public float gravity = -15f;      // Trọng lực kéo xuống

    private Animator anim;
    private bool isOpeningChest = false;
    private bool isJumping = false;
    private float verticalVelocity = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (isOpeningChest) return;

        float h = 0f;
        float v = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) v = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) v = -1f;

        Vector3 moveDirection = new Vector3(h, 0f, v).normalized;
        bool isMoving = moveDirection.magnitude > 0.1f;

        // --- Di chuyển ngang (vẫn hoạt động kể cả khi đang nhảy) ---
        if (isMoving)
        {
            Vector3 horizontalMove = moveDirection * speed * Time.deltaTime;
            transform.Translate(horizontalMove, Space.World);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // --- Xử lý nhảy (chỉ khi không đang mở rương) ---
        if (!isOpeningChest && Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            isJumping = true;
            verticalVelocity = jumpForce;
            anim.SetTrigger("Jump");
        }

        // --- Áp dụng trọng lực + di chuyển lên/xuống khi đang nhảy ---
        if (isJumping)
        {
            verticalVelocity += gravity * Time.deltaTime;
            transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime, Space.World);

            // Chạm đất (giả sử mặt đất ở Y = 0, chỉnh lại nếu khác)
            if (transform.position.y <= 0f)
            {
                Vector3 pos = transform.position;
                pos.y = 0f;
                transform.position = pos;
                isJumping = false;
                verticalVelocity = 0f;
            }
        }

        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);

            if (Keyboard.current.eKey.wasPressedThisFrame && !isMoving && !isJumping)
            {
                StartCoroutine(OpenChestRoutine());
            }
        }
    }

    System.Collections.IEnumerator OpenChestRoutine()
    {
        isOpeningChest = true;
        anim.SetBool("isMoving", false);
        anim.SetTrigger("OpenChest");

        yield return new WaitForSeconds(openChestDuration);

        isOpeningChest = false;
    }
}