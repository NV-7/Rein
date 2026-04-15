using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Drag your Player here in the inspector

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Height and distance behind player
    public float mouseSensitivity = 3f;

    [Header("Pitch Limits (Up/Down)")]
    public float minPitch = -40f;
    public float maxPitch = 60f;

    private float pitch = 0f;
    private float yaw = 0f;

    void Start()
    {
        // Lock and hide the mouse cursor for a better gameplay experience
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get mouse inputs
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Clamp the up/down rotation so the camera doesn't flip upside down
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calculate new rotation and position
        Quaternion currentRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = target.position + currentRotation * offset;

        // Apply to camera
        transform.position = targetPosition;

        // Make the camera look at the player's upper body/head area
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}