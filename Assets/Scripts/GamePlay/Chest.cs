using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Chest Objects")]
    public GameObject closedChest;
    public GameObject openedChest;

    [Header("Interaction")]
    public float interactionDistance = 2f;

    [Header("Treasure")]
    public bool requireAllKeys = false;
    public bool isFinalTreasure = false;

    [Header("Congratulations")]
    public CongratulationsUI congratulationsUI;

    private bool isOpen = false;

    void Start()
    {
        isOpen = false;

        if (closedChest != null)
        {
            closedChest.SetActive(true);
        }

        if (openedChest != null)
        {
            openedChest.SetActive(false);
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public bool CanOpen()
    {
        if (!requireAllKeys)
        {
            return true;
        }

        if (GameManager.Instance == null)
        {
            return false;
        }

        return GameManager.Instance.HasAllKeys();
    }

    public void OpenChest()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;

        if (closedChest != null)
        {
            closedChest.SetActive(false);
        }

        if (openedChest != null)
        {
            openedChest.SetActive(true);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayChestOpenSound();
        }

        if (isFinalTreasure)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowCongratulations();
            }
            else if (congratulationsUI != null)
            {
                congratulationsUI.ShowCongratulations();
            }
        }
    }
}