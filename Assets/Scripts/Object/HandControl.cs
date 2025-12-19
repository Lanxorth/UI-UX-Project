using UnityEngine;

public class HandController : MonoBehaviour
{
    public HandPreview handPreview;

    // La main active pour prendre un objet (déterminée par UI)
    bool leftHandActive = true;

    public void SetActiveHand(bool leftHand)
    {
        leftHandActive = leftHand;
    }

    public void PickUpObject(GameObject obj)
    {
        if (handPreview != null)
            handPreview.ShowInHand(obj, leftHandActive);
    }

    public void DropObject()
    {
        if (handPreview != null)
            handPreview.RemoveFromHand(leftHandActive);

    }

    public void PickSelectedObject()
    {
        Debug.Log("BOUTON CLIQUÉ");

        ObjectSelector selector = FindObjectOfType<ObjectSelector>();
        if (selector == null)
        {
            Debug.LogError("ObjectSelector introuvable");
            return;
        }

        GameObject obj = selector.GetSelectedObject();
        if (obj == null)
        {
            Debug.LogError("Aucun objet sélectionné");
            return;
        }

        Debug.Log("Objet pris : " + obj.name);

        PickUpObject(obj);
        selector.ClearSelection();
    }


}
