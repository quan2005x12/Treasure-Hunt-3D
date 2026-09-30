using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Congratulations")]
    public GameObject congratulationsPanel;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip keyPickupSound;
    public AudioClip portalSound;
    public AudioClip chestOpenSound;
    public AudioClip victorySound;

    [Header("Scene Transition")]
    public CanvasGroup transitionCanvasGroup;

    public float fadeInDuration = 0.5f;

    private bool[] collectedKeys = new bool[3];

    private string message = "";
    private float messageTimer = 0f;

    private bool isTransitioning = false;

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

        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.alpha = 0f;
            transitionCanvasGroup.interactable = false;
            transitionCanvasGroup.blocksRaycasts = false;
        }
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

    public void PlayPortalSound()
    {
        PlaySound(
            portalSound
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

    public void StartSceneTransition(
        string nextSceneName)
    {
        if (isTransitioning)
        {
            return;
        }

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError(
                "Tên Scene tiếp theo đang trống."
            );

            return;
        }

        StartCoroutine(
            SceneTransitionRoutine(
                nextSceneName
            )
        );
    }

    IEnumerator SceneTransitionRoutine(
        string nextSceneName)
    {
        isTransitioning = true;

        Debug.Log(
            "Bắt đầu chuyển Scene: " +
            nextSceneName
        );

        // ========================================
        // 1. TẮT BGM MAP HIỆN TẠI
        // ========================================

        StopBackgroundMusic();

        // ========================================
        // 2. HIỆN MÀN HÌNH ĐEN NGAY LẬP TỨC
        // ========================================

        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.alpha = 1f;

            transitionCanvasGroup.interactable = false;
            transitionCanvasGroup.blocksRaycasts = true;
        }

        // ========================================
        // 3. PHÁT ÂM THANH PORTAL
        // ========================================

        PlayPortalSound();

        float portalSoundLength = 0f;

        if (portalSound != null)
        {
            portalSoundLength =
                portalSound.length;
        }

        // ========================================
        // 4. BẮT ĐẦU LOAD SCENE NGAY
        // ========================================

        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(
                nextSceneName
            );

        if (asyncLoad == null)
        {
            Debug.LogError(
                "Không thể Load Scene: " +
                nextSceneName
            );

            isTransitioning = false;

            yield break;
        }

        // Không cho Scene mới hiển thị ngay.
        asyncLoad.allowSceneActivation = false;

        // ========================================
        // 5. CHỜ SCENE LOAD XONG Ở PHÍA SAU
        // ========================================

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        Debug.Log(
            "Scene mới đã Load xong ở phía sau."
        );

        // ========================================
        // 6. CHỜ PORTAL AUDIO PHÁT XONG
        // ========================================

        if (portalSoundLength > 0f)
        {
            yield return new WaitForSeconds(
                portalSoundLength
            );
        }

        // ========================================
        // 7. CHO SCENE MỚI HIỆN RA
        // ========================================

        asyncLoad.allowSceneActivation = true;

        yield return null;

        Debug.Log(
            "Đã chuyển sang Scene mới."
        );

        // ========================================
        // 8. FADE MÀN HÌNH ĐEN → MAP MỚI
        // ========================================

        if (transitionCanvasGroup != null)
        {
            float timer = 0f;

            while (timer < fadeInDuration)
            {
                timer += Time.deltaTime;

                float alpha =
                    1f -
                    Mathf.Clamp01(
                        timer /
                        fadeInDuration
                    );

                transitionCanvasGroup.alpha =
                    alpha;

                yield return null;
            }

            transitionCanvasGroup.alpha = 0f;

            transitionCanvasGroup.interactable = false;
            transitionCanvasGroup.blocksRaycasts = false;
        }

        isTransitioning = false;

        Debug.Log(
            "Hoàn tất hiệu ứng chuyển Scene."
        );
    }

    public void ShowCongratulations()
    {
        HideMessage();

        StopBackgroundMusic();

        PlaySound(
            victorySound
        );

        if (congratulationsPanel != null)
        {
            congratulationsPanel.SetActive(true);
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