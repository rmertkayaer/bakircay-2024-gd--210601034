using UnityEngine;

public class Items : MonoBehaviour
{
    private static bool isDragging = false;
    private Rigidbody rb;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float initialY;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialY = transform.position.y;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (isDragging) return; // Prevent dragging if another object is being dragged
        isDragging = true;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        cursorPosition.y = 2.0f; // y değerini sabit 2 olarak ayarlayın
        rb.MovePosition(cursorPosition);
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.useGravity = true;
    }

    public void ReturnToInitialPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}