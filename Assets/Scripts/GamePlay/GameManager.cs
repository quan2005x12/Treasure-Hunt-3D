using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Congratulations")]
    public GameObject congratulationsPanel;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip keyPickupSound;
    public AudioClip chestOpenSound;
    public AudioClip victorySound;

    private bool[] collectedKeys = new bool[3];

    private string message = "";
    private float messageTimer = 0f;

    void Awake()
    {
        if (Instance != null &&
            Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0f)
            {
                message = "";
            }
        }
    }

    void OnGUI()
    {
        GUIStyle keyStyle =
            new GUIStyle(GUI.skin.label);

        keyStyle.fontSize = 26;
        keyStyle.fontStyle =
            FontStyle.Bold;

        GUI.Label(
            new Rect(
                20,
                20,
                250,
                50
            ),
            "Chìa khóa: " +
            GetKeyCount() +
            "/3",
            keyStyle
        );

        if (!string.IsNullOrEmpty(message))
        {
            GUIStyle messageStyle =
                new GUIStyle(GUI.skin.label);

            messageStyle.fontSize = 28;

            messageStyle.alignment =
                TextAnchor.MiddleCenter;

            GUI.Label(
                new Rect(
                    0,
                    Screen.height - 100,
                    Screen.width,
                    50
                ),
                message,
                messageStyle
            );
        }
    }

    public void ShowMessage(
        string text,
        float duration = 2f)
    {
        message = text;
        messageTimer = duration;
    }

    public void HideMessage()
    {
        message = "";
        messageTimer = 0f;
    }

    public void PlaySound(
        AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogWarning(
                "GameManager chưa gán AudioSource."
            );

            return;
        }

        if (clip == null)
        {
            Debug.LogWarning(
                "AudioClip chưa được gán."
            );

            return;
        }

        audioSource.PlayOneShot(
            clip
        );
    }

    public void PlayChestOpenSound()
    {
        PlaySound(
            chestOpenSound
        );
    }

    public void StopBackgroundMusic()
    {
        GameObject bgmObject =
            GameObject.Find("BGM");

        if (bgmObject == null)
        {
            Debug.LogWarning(
                "Không tìm thấy GameObject BGM."
            );

            return;
        }

        AudioSource bgmAudioSource =
            bgmObject.GetComponent<AudioSource>();

        if (bgmAudioSource == null)
        {
            Debug.LogWarning(
                "BGM không có AudioSource."
            );

            return;
        }

        bgmAudioSource.Stop();

        Debug.Log(
            "Đã tắt nhạc nền."
        );
    }

    public void ShowCongratulations()
    {
        HideMessage();

        StopBackgroundMusic();

        PlaySound(
            victorySound
        );

        // Tìm CongratulationsUI trong Scene hiện tại
        // kể cả khi Panel đang bị inactive.
        CongratulationsUI congratulationsUI =
            FindFirstObjectByType<CongratulationsUI>(
                FindObjectsInactive.Include
            );

        if (congratulationsUI != null)
        {
            congratulationsUI.ShowCongratulations();

            Debug.Log(
                "Đã hiển thị giao diện CHÚC MỪNG."
            );
        }
        else if (congratulationsPanel != null)
        {
            // Phương án dự phòng
            congratulationsPanel.SetActive(true);

            Debug.Log(
                "Đã hiển thị Congratulations Panel."
            );
        }
        else
        {
            Debug.LogError(
                "Không tìm thấy CongratulationsUI hoặc CongratulationsPanel."
            );
        }

        Debug.Log(
            "CHÚC MỪNG! Người chơi đã hoàn thành game."
        );
    }

    public void CollectKey(
        int keyID)
    {
        if (keyID < 1 ||
            keyID > 3)
        {
            Debug.LogError(
                "Key ID phải từ 1 đến 3."
            );

            return;
        }

        if (collectedKeys[keyID - 1])
        {
            return;
        }

        collectedKeys[keyID - 1] = true;

        PlaySound(
            keyPickupSound
        );

        Debug.Log(
            "Đã nhặt Key " +
            keyID +
            ". Tổng số Key: " +
            GetKeyCount() +
            "/3"
        );
    }

    public bool HasKey(
        int keyID)
    {
        if (keyID < 1 ||
            keyID > 3)
        {
            return false;
        }

        return collectedKeys[keyID - 1];
    }

    public int GetKeyCount()
    {
        int count = 0;

        for (int i = 0;
             i < collectedKeys.Length;
             i++)
        {
            if (collectedKeys[i])
            {
                count++;
            }
        }

        return count;
    }

    public bool HasAllKeys()
    {
        return collectedKeys[0] &&
               collectedKeys[1] &&
               collectedKeys[2];
    }
}