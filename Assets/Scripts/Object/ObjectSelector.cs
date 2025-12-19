using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ObjectSelector : MonoBehaviour
{
    public LayerMask interactableLayer;
    public float maxDistance = 100f;

    public HandController handController;
    public GameObject handChoiceUI; // panneau avec boutons gauche / droite

    Camera cam;
    GameObject selectedObject;

    void Awake()
    {
        cam = Camera.main;
        if (handChoiceUI != null)
            handChoiceUI.SetActive(false);
    }

    void Update()
    {
        // si la souris est sur l'UI, on ne sélectionne pas d'objet
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
            {
                SelectObject(hit.collider.gameObject);
            }
        }
    }

    void SelectObject(GameObject obj)
    {
        selectedObject = obj;

        // afficher le choix de la main
        if (handChoiceUI != null)
            handChoiceUI.SetActive(true);
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public void ClearSelection()
    {
        selectedObject = null;
        if (handChoiceUI != null)
            handChoiceUI.SetActive(false);
    }
}
