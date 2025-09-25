using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    Transform _snapPoint;
    [HideInInspector]public bool _isSomethingInPoint = false;
    [Range (0, 7)]public int _idObjectAtPoint;

    private void Awake()
    {
        _snapPoint = transform;
    }
}
