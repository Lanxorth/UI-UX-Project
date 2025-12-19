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
            handPreview.HideHand(leftHandActive);
    }

    public void PickSelectedObject(bool leftHand)
    {
        ObjectSelector selector = FindObjectOfType<ObjectSelector>();
        GameObject obj = selector.GetSelectedObject();

        if (obj == null) return;

        SetActiveHand(leftHand);
        PickUpObject(obj);

        selector.ClearSelection();
    }

}
