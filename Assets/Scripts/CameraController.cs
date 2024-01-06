using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset;

    void LateUpdate()
    {
        if (target)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = new Vector3(targetPosition.x, targetPosition.y, offset.z);
        }
    }
}
