using UnityEngine;
using TMPro;

public class KeyMessageUI : MonoBehaviour
{
    public static KeyMessageUI Instance;

    [Header("UI")]
    public TMP_Text messageText;

    void Awake()
    {
        Instance = this;

        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    public void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    public void HideMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }
}