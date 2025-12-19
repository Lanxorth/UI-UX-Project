using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelector : MonoBehaviour
{
    [Header("Raycast")]
    public LayerMask interactableLayer;
    public float maxDistance = 100f;

    [Header("World Space Menu")]
    public GameObject worldMenu;
    public Vector3 menuOffset = new Vector3(0, 0.5f, 0);

    Camera cam;
    GameObject selectedObject;

    void Awake()
    {
        cam = Camera.main;
        if (worldMenu != null)
            worldMenu.SetActive(false);
    }

    void Update()
    {
        HandleClickSelection();
    }

    void HandleClickSelection()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
            {
                Select(hit.collider.gameObject, hit.point);
            }
            else
            {
                Deselect();
            }
        }
    }

    void Select(GameObject obj, Vector3 hitPoint)
    {
        selectedObject = obj;

        // Récupérer le HandController
        HandController hands = FindObjectOfType<HandController>();
        if (hands != null)
        {
            hands.PickUpObject(obj); // par exemple main gauche
        }

        if (worldMenu != null)
        {
            worldMenu.transform.position = hitPoint + menuOffset;
            worldMenu.SetActive(true);
        }

        Debug.Log("Objet sélectionné : " + obj.name);
    }


    void Deselect()
    {
        selectedObject = null;

        if (worldMenu != null)
            worldMenu.SetActive(false);
    }

    // Accès pour les autres scripts (menu, preview, placement…)
    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }
}
