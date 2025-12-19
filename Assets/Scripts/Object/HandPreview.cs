using UnityEngine;
using UnityEngine.UI;

public class HandPreview : MonoBehaviour
{
    public Camera leftHandCamera;
    public Camera rightHandCamera;

    public RawImage leftHandImage;
    public RawImage rightHandImage;

    public GameObject leftHandObject;
    public GameObject rightHandObject;

    public float rotationSpeed = 40f;

    void Update()
    {
        if (leftHandObject != null)
            leftHandObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (rightHandObject != null)
            rightHandObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void ShowInHand(GameObject obj, bool leftHand)
    {
        if (obj == null) return;

        // une main = un objet
        if (leftHand && leftHandObject != null) return;
        if (!leftHand && rightHandObject != null) return;

        Camera cam = leftHand ? leftHandCamera : rightHandCamera;
        RawImage image = leftHand ? leftHandImage : rightHandImage;

        // parent à la caméra de la main
        obj.transform.SetParent(cam.transform);

        // position locale devant la caméra
        obj.transform.localPosition = new Vector3(0, 0, 0.5f);
        obj.transform.localRotation = Quaternion.identity;

        // changer de layer
        obj.layer = LayerMask.NameToLayer(
            leftHand ? "LeftHand" : "RightHand"
        );

        // désactiver la physique
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        if (leftHand) leftHandObject = obj;
        else rightHandObject = obj;

        image.texture = cam.targetTexture;
    }

    public void RemoveFromHand(bool leftHand)
    {
        if (leftHand && leftHandObject != null)
            leftHandObject = null;

        if (!leftHand && rightHandObject != null)
            rightHandObject = null;
    }
}
