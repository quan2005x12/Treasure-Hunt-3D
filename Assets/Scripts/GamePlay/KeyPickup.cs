using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPickup : MonoBehaviour
{
    [Header("Key")]
    public int keyID = 1;

    [Header("Portal")]
    public GameObject portal;

    private bool playerNearKey = false;
    private bool isCollected = false;

    void Start()
    {
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerNearKey)
        {
            return;
        }

        if (isCollected)
        {
            return;
        }

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            CollectKey();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerNearKey = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowMessage(
                "Phát hiện chìa khóa! Nhấn E để nhặt."
            );
        }

        Debug.Log(
            "Phát hiện Key " +
            keyID
        );
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerNearKey = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.HideMessage();
        }
    }

    void CollectKey()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "Không tìm thấy GameManager."
            );

            return;
        }

        GameManager.Instance.CollectKey(
            keyID
        );

        isCollected = true;

        GameManager.Instance.ShowMessage(
            "Đã nhặt được chìa khóa!"
        );

        if (portal != null)
        {
            portal.SetActive(true);

            Debug.Log(
                "Cổng đã xuất hiện."
            );
        }
        else
        {
            Debug.LogError(
                "Key chưa được gán Portal."
            );
        }

        gameObject.SetActive(false);
    }
}