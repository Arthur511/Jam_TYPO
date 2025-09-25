using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] List<SpawnableObject> _spawnableObjects;
    [SerializeField] Letters _lettersForLevel;

    [SerializeField] Transform _spawnPointObject;

    HashSet<string> _alreadySpawnables = new HashSet<string>();

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

        if (_alreadySpawnables.Contains(_lowerInput))
        {
            _inputField.text = null;
            return;
        }

        if (_isInList)
        {
            foreach (var spawnableObject in _spawnableObjects)
            {
                if (_lowerInput == spawnableObject._objectName.ToLower())
                {
                    Instantiate(spawnableObject._prefabObject, _spawnPointObject);
                    _alreadySpawnables.Add(_lowerInput);
                    break;
                }
            }
        }
        _inputField.text = null;
    }
}
