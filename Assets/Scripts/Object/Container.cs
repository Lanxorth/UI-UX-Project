using UnityEngine;

public class Container : MonoBehaviour
{
    public Transform placePoint;         // point où l'objet sera posé
    public GameObject handChoiceUI;      // panel avec boutons Main gauche / droite

    public void OnContainerClicked()
    {
        if (handChoiceUI != null)
        {
            // positionne le panel au-dessus du conteneur
            handChoiceUI.transform.position = placePoint.position + new Vector3(0, 0.5f, 0);
            handChoiceUI.SetActive(true);
        }
    }

    // appelé par le bouton Main gauche (true) ou droite (false)
    public void PlaceObjectInContainer(bool leftHand)
    {
        HandPreview handPreview = FindObjectOfType<HandPreview>();
        if (handPreview == null) return;

        GameObject objToPlace = leftHand ? handPreview.leftHandObject : handPreview.rightHandObject;
        if (objToPlace == null) return;

        // positionne dans le conteneur
        objToPlace.transform.SetParent(placePoint);
        objToPlace.transform.localPosition = Vector3.zero;
        objToPlace.transform.localRotation = Quaternion.identity;

        // remet la physique et le layer monde
        objToPlace.layer = LayerMask.NameToLayer("Interactable");
        Rigidbody rb = objToPlace.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

        // vide la main
        handPreview.RemoveFromHand(leftHand);

        // cache le panel
        if (handChoiceUI != null)
            handChoiceUI.SetActive(false);
    }
}
