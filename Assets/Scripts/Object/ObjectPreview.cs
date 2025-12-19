using UnityEngine;
using UnityEngine.UI;

public class ObjectPreview : MonoBehaviour
{
    public Camera previewCamera;      // caméra dédiée
    public RawImage previewImage;     // UI RawImage
    public Vector3 cameraOffset = new Vector3(0, 0, -2f); // position relative à l'objet
    public float rotationSpeed = 30f; // rotation automatique (optionnel)

    GameObject currentObject;

    void Update()
    {
        if (currentObject != null)
        {
            // rotation optionnelle pour effet 3D
            currentObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ShowObject(GameObject obj)
    {
        if (obj == null)
        {
            HidePreview();
            return;
        }

        currentObject = obj;

        // position de la caméra par rapport à l'objet
        previewCamera.transform.position = currentObject.transform.position + cameraOffset;
        previewCamera.transform.LookAt(currentObject.transform);

        if (previewImage != null)
            previewImage.texture = previewCamera.targetTexture;
    }

    public void HidePreview()
    {
        currentObject = null;
        if (previewImage != null)
            previewImage.texture = null;
    }
}
