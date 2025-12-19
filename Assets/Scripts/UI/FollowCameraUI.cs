using UnityEngine;

public class FollowCameraUI : MonoBehaviour
{
    public Camera targetCamera;
    public float distance = 2f;

    void LateUpdate()
    {
        if (targetCamera == null) return;

        transform.position = targetCamera.transform.position +
                             targetCamera.transform.forward * distance;

        transform.rotation = Quaternion.LookRotation(
            transform.position - targetCamera.transform.position
        );
    }
}
