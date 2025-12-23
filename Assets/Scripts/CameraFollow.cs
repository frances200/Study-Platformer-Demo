using UnityEngine;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Default Z offset

    [Header("Settings")]
    public float smoothTime = 0.25f; // Time (in seconds) to reach the target. Approx 0.2s - 0.3s is good.
    private Vector3 velocity = Vector3.zero; // Used internally by SmoothDamp

    [Header("Boundaries")]
    public bool enableLimits = true;
    public Vector2 minPosition; // Bottom-Left limit
    public Vector2 maxPosition; // Top-Right limit

    void LateUpdate()
    {
        // 1. Define the target position
        Vector3 targetPosition = target.position + offset;

        // 2. Clamp the target position to your boundaries
        // We do this BEFORE moving the camera so it slows down naturally as it approaches the edge
        if (enableLimits)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
        }

        // 3. SmoothDamp
        // It takes: Current Position, Target Position, Current Velocity (ref), and the Smooth Time
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // BONUS: This draws a red box in the Scene view so you can see your boundaries visually!
    private void OnDrawGizmos()
    {
        if (enableLimits)
        {
            Gizmos.color = Color.red;
            // Draw a box representing the area the camera center is allowed to exist in
            Vector3 center = new Vector3((minPosition.x + maxPosition.x) / 2, (minPosition.y + maxPosition.y) / 2, 0);
            Vector3 size = new Vector3(maxPosition.x - minPosition.x, maxPosition.y - minPosition.y, 1);
            Gizmos.DrawWireCube(center, size);
        }
    }
}