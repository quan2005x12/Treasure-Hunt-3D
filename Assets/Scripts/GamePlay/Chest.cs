
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Chest Objects")]
    public GameObject closedChest;
    public GameObject openedChest;

    [Header("Interaction")]
    public float interactionDistance = 2f;

    private bool isOpen = false;

    public bool IsOpen()
    {
        return isOpen;
    }

    public bool CanOpen(Vector3 playerPosition)
    {
        if (isOpen)
        {
            return false;
        }

        float distance =
            Vector3.Distance(
                playerPosition,
                transform.position
            );

        return distance <= interactionDistance;
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
    }
}