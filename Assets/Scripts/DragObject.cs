using UnityEngine;

public class DragObject : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask _snapMask;

    GameObject _currentDragObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentDragObject == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (CheckLayer(hit.collider.gameObject.layer, mask) == false)
                    {
                        return;
                    }
                    _currentDragObject = hit.collider.gameObject;
                    if (_currentDragObject.GetComponent<ObjectInformation>().CurrentSnapPoint != null)
                    {
                        _currentDragObject.GetComponent<ObjectInformation>().CurrentSnapPoint._isSomethingInPoint = false;
                        _currentDragObject.GetComponent<ObjectInformation>().CurrentSnapPoint = null;
                    }
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(_currentDragObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                _currentDragObject.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);

                RaycastHit hit2;
                Physics.Raycast(_currentDragObject.transform.position, Vector3.up, out hit2, 100, _snapMask);
                if (hit2.collider != null)
                {
                    if (hit2.collider.gameObject.TryGetComponent<SnapPoint>(out SnapPoint point2))
                    {
                        if (!point2._isSomethingInPoint)
                        {
                            _currentDragObject.transform.position = point2.gameObject.transform.position;
                            if (point2._idObjectAtPoint == _currentDragObject.GetComponent<ObjectInformation>().ObjectID)
                            {
                                _currentDragObject.layer = 0;
                                MainGame.instance._placementToComplete++;
                                if (MainGame.instance._placementToComplete == MainGame.instance.ScoreToReach)
                                {
                                    MainGame.instance.WinRoom();
                                }
                            }
                            _currentDragObject.GetComponent<ObjectInformation>().CurrentSnapPoint = point2;
                            point2._isSomethingInPoint = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                _currentDragObject = null;
                Cursor.visible = true;
            }
        }

        if (_currentDragObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(_currentDragObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            _currentDragObject.transform.position = new Vector3(worldPosition.x, 2f, worldPosition.z);
        }
    }

    private void OnDrawGizmos()
    {
        if (_currentDragObject != null)
        {
            Gizmos.DrawLine(_currentDragObject.transform.position, _currentDragObject.transform.position + Vector3.back * 10);
        }
    }

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
