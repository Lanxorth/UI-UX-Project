using UnityEngine;
using UnityEngine.UI;

public class HandPreview : MonoBehaviour
{
    [Header("Cameras et UI")]
    public Camera leftHandCamera;
    public Camera rightHandCamera;
    public RawImage leftHandImage;
    public RawImage rightHandImage;

    [Header("Options")]
    public Vector3 cameraOffset = new Vector3(0, 0, -2f);
    public float rotationSpeed = 30f;

    GameObject leftHandObject;
    GameObject rightHandObject;

    // Afficher un objet dans la main choisie
    public void ShowInHand(GameObject obj, bool leftHand)
    {
        if (obj == null)
        {
            HideHand(leftHand);
            return;
        }

        Camera cam = leftHand ? leftHandCamera : rightHandCamera;
        RawImage image = leftHand ? leftHandImage : rightHandImage;

        // Positionner l'objet devant la caméra
        obj.transform.position = cam.transform.position - cam.transform.forward * cameraOffset.z;
        obj.transform.rotation = Quaternion.identity;

        // Définir le layer approprié pour que seule la caméra cible voie l’objet
        obj.layer = LayerMask.NameToLayer(leftHand ? "HeldObjectLeft" : "HeldObjectRight");

        // Stocker l'objet
        if (leftHand) leftHandObject = obj;
        else rightHandObject = obj;

        // Activer la RenderTexture dans l'UI
        if (image != null)
            image.texture = cam.targetTexture;
    }

    void Update()
    {
        // rotation pour effet 3D
        if (leftHandObject != null)
            leftHandObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        if (rightHandObject != null)
            rightHandObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    // Cacher l'objet de la main
    public void HideHand(bool leftHand)
    {
        GameObject obj = leftHand ? leftHandObject : rightHandObject;
        RawImage image = leftHand ? leftHandImage : rightHandImage;

        if (obj != null)
        {
            obj.layer = LayerMask.NameToLayer("Default");
            if (leftHand) leftHandObject = null;
            else rightHandObject = null;
        }

        if (image != null)
            image.texture = null;
    }
}
