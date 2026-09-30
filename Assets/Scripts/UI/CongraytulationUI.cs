using UnityEngine;

public class CongratulationsUI : MonoBehaviour
{
    public GameObject congratulationsPanel;

    void Start()
    {
        if (congratulationsPanel != null)
        {
            congratulationsPanel.SetActive(false);
        }
    }

    public void ShowCongratulations()
    {
        if (congratulationsPanel != null)
        {
            congratulationsPanel.SetActive(true);
        }
    }
}