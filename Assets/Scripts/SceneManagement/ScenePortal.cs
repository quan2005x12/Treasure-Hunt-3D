using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    [Header("Next Scene")]
    public string nextSceneName;

    private bool isLoading = false;

    void OnTriggerEnter(Collider other)
    {
        if (isLoading)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError(
                "Chưa nhập tên Scene tiếp theo."
            );

            return;
        }

        isLoading = true;

        Debug.Log(
            "Người chơi đã bước vào Portal."
        );

        // Chuyển Scene ngay lập tức
        SceneManager.LoadScene(
            nextSceneName
        );
    }
}