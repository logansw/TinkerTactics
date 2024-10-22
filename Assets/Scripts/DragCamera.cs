using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Vector3 dragOrigin; // Store the initial click position

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // When the left mouse button is clicked
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Capture the world point of where the mouse was clicked
            return;
        }

        if (!Input.GetMouseButton(1)) return; // If the mouse isn't held, do nothing

        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the current mouse position in world space
        Vector3 difference = dragOrigin - currentPos; // Calculate the difference in world units

        // Move the camera by the difference
        Camera.main.transform.position += difference;

        // Update the dragOrigin for continuous movement
        dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
