using UnityEngine;
using UnityEngine.InputSystem;

public class ContainerSelector : MonoBehaviour
{
    public LayerMask containerLayer;
    public Camera playerCamera;

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 5f, containerLayer))
            {
                Container container = hit.collider.GetComponent<Container>();
                if (container != null)
                {
                    container.OnContainerClicked();
                }
            }
        }
    }
}
