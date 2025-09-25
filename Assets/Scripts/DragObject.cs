using UnityEngine;

public class DragObject : MonoBehaviour
{
    public LayerMask mask;

    GameObject _currentDragObject;
    bool CheckLayer(int layer, LayerMask layerMask)
    {
        if (((1 << layer) & layerMask.value) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentDragObject == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    Debug.Log("IsDrag");
                    if (CheckLayer(hit.collider.gameObject.layer, mask) == false)
                    {
                        return;
                    }
                    _currentDragObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            else
            {

            }
        }

        if (_currentDragObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(_currentDragObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            _currentDragObject.transform.position = new Vector2(worldPosition.x, worldPosition.y);
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
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }

}
