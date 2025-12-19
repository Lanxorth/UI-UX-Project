using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHover : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Material highlightMaterial;
    public Book book; // Référence au script Book

    Camera cam;
    Renderer currentRenderer;
    Material originalMaterial;

    void Start()
    {
        cam = Camera.main;
        if (cam == null)
            Debug.LogError("Aucune MainCamera trouvée !");
    }

    void Update()
    {
        // Bloque le raycast si un popup est ouvert
        if (book != null && book.isPopupOpen)
        {
            ClearHighlight();
            return;
        }

        if (cam == null || Mouse.current == null)
            return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red);

        if (Physics.Raycast(ray, out hit, 50f, interactableLayer))
        {
            Renderer r = hit.collider.GetComponent<Renderer>();

            if (r != null && r != currentRenderer)
            {
                ClearHighlight();

                originalMaterial = r.material;
                r.material = highlightMaterial;
                currentRenderer = r;

                Debug.Log("Hover sur : " + r.name);
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (currentRenderer != null)
        {
            currentRenderer.material = originalMaterial;
            currentRenderer = null;
        }
    }
}
