using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSimple : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;
    public Transform cameraTransform;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Souris TOUJOURS libre
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float forward = 0f;

        if (Keyboard.current.zKey.isPressed || Keyboard.current.wKey.isPressed)
            forward = 1f;

        if (Keyboard.current.sKey.isPressed)
            forward = -1f;

        Vector3 move = transform.forward * forward * moveSpeed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }

    void Rotate()
    {
        float turn = 0f;

        if (Keyboard.current.qKey.isPressed || Keyboard.current.aKey.isPressed)
            turn = -1f;

        if (Keyboard.current.dKey.isPressed)
            turn = 1f;

        transform.Rotate(Vector3.up * turn * rotationSpeed * Time.fixedDeltaTime);
    }
}
