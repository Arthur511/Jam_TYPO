using UnityEngine;

[CreateAssetMenu]
public class SpawnableObject : ScriptableObject
{
    public string _objectName;
    public GameObject _prefabObject;
    public bool _correctPosition = false;
}
