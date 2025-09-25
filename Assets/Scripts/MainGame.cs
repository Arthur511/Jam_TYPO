using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] List<SpawnableObject> _spawnableObjects;
    [SerializeField] Letters _lettersForLevel;

    [SerializeField] Transform _spawnPointObject;

    bool _isInList = true;
    public void OnSubmit()
    {
        _isInList = true;
        string _lowerInput = _inputField.text.ToLower().Trim();
        for (int i = 0; i < _lowerInput.Length; i++)
        {
            if (!_lettersForLevel._levelLetters.Contains(_lowerInput[i]))
            {
                _isInList = false;
                break;
            }
        }
        if (_isInList)
        {
            foreach (var spawnableObject in _spawnableObjects)
            {
                if (_lowerInput == spawnableObject._objectName.ToLower())
                {
                    Instantiate(spawnableObject._prefabObject, _spawnPointObject);
                }
            }
        }
        _inputField.text = null;
    }
}
