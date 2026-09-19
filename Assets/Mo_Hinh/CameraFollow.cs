using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Nhân vật cần theo dõi")]
    public Transform target;

    [Header("Khoảng cách camera với nhân vật (offset)")]
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    [Header("Độ mượt khi camera đuổi theo (số nhỏ = mượt hơn)")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    [Header("Có luôn nhìn về phía nhân vật không")]
    public bool lookAtTarget = true;

    void LateUpdate()
    {
        if (target == null) return;

        // Vị trí mong muốn = vị trí nhân vật + offset
        Vector3 desiredPosition = target.position + offset;

        // Di chuyển camera mượt tới vị trí đó
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Xoay camera nhìn về nhân vật (tuỳ chọn)
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
