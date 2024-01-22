using UnityEngine;

public class CameraFollowCharacterAndCursor : MonoBehaviour
{
    public Transform characterTransform;
    public Transform cursorTransform;
    public float cameraDistance = 10f;
    public float cameraLerpSpeed = 5f;
    public float maxOffsetX = 5f;
    public float maxOffsetY = 3f;

    private void LateUpdate()
    {
        if (characterTransform == null || cursorTransform == null)
        {
            Debug.LogWarning("Character or cursor transform is not assigned.");
            return;
        }

        // Calculate the midpoint between character and cursor
        Vector3 midpoint = (characterTransform.position + cursorTransform.position) * 0.5f;

        // Calculate the desired camera position based on midpoint and distance
        Vector3 cameraPosition = new Vector3(midpoint.x, midpoint.y, transform.position.z) - Vector3.forward * cameraDistance;

        // Calculate the offset between the camera and character
        float offsetX = Mathf.Clamp(cameraPosition.x - characterTransform.position.x, -maxOffsetX, maxOffsetX);
        float offsetY = Mathf.Clamp(cameraPosition.y - characterTransform.position.y, -maxOffsetY, maxOffsetY);

        // Apply the offset to the camera position
        cameraPosition = new Vector3(characterTransform.position.x + offsetX, characterTransform.position.y + offsetY, -10);

        // Smoothly interpolate towards the desired camera position
        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraLerpSpeed * Time.deltaTime);
    }
}