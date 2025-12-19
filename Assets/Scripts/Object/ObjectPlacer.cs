using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    [Header("Raycast")]
    public Camera playerCamera;
    public LayerMask containerLayer; // Layer des conteneurs
    public float maxDistance = 5f;

    void Update()
    {
        if (Mouse.current == null) return;

        // clic gauche
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, containerLayer))
            {
                Container container = hit.collider.GetComponent<Container>();
                if (container != null)
                {
                    // affiche le panel Main gauche / Main droite
                    container.OnContainerClicked();
                }
            }
        }
    }
}
