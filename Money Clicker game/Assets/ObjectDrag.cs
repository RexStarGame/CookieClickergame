using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private float deltaX, deltaY;
    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        GetComponent<PlaceableObject>().CheckPlacement();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}