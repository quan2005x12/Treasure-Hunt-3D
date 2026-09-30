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

        isLoading = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartSceneTransition(
                nextSceneName
            );
        }
        else
        {
            Debug.LogError(
                "Không tìm thấy GameManager."
            );
        }
    }
}