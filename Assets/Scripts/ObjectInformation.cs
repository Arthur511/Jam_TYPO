using UnityEngine;

public class ObjectInformation : MonoBehaviour
{
    [Range(0, 7)]
    public int ObjectID;
    [HideInInspector] public SnapPoint CurrentSnapPoint;
}
