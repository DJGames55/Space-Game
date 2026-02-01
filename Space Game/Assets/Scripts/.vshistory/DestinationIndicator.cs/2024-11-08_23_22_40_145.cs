using UnityEngine;
using UnityEngine.UI;

public class DestinationIndicator : MonoBehaviour
{
    public Transform destination; // Set this to the target destination
    public RectTransform uiElement; // Assign the UI indicator for this destination
    public Camera mainCamera;
    public float edgePadding = 50f; // Padding from the edge of the screen

    void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(destination.position);

        // Check if the destination is in front of the player
        if (screenPos.z > 0)
        {
            // Clamp position to screen edges with padding
            screenPos.x = Mathf.Clamp(screenPos.x, edgePadding, Screen.width - edgePadding);
            screenPos.y = Mathf.Clamp(screenPos.y, edgePadding, Screen.height - edgePadding);

            // Set the position of the UI element
            uiElement.position = screenPos;
        }
        else
        {
            // If the target is behind the player, hide the UI element or display it at the edge
            screenPos.x = Screen.width - screenPos.x;
            screenPos.y = Screen.height - screenPos.y;
            uiElement.position = screenPos;
        }

        // Optionally, you could rotate the icon based on the direction (e.g., point the arrow at the destination)
    }
}
