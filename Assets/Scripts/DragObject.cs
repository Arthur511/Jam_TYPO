using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject _currentDragObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentDragObject != null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                }
            }
            else
            {

            }
        }

        if (_currentDragObject != null)
        {

        }
    }


    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar  - worldMousePosNear, out hit);
        return hit;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("IsBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("IsDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }

}
