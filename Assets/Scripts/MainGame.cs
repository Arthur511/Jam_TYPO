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
        for (int i = 0; i < _inputField.text.Length; i++)
        {
            if (!_lettersForLevel._levelLetters.Contains(_inputField.text[i]))
            {
                _isInList = false;
            }
        }
        if (_isInList)
        {
            foreach (var spawnableObject in _spawnableObjects)
            {

                if (_inputField.text == spawnableObject._objectName)
                {
                    Instantiate(spawnableObject._prefabObject, _spawnPointObject);
                }
            }
        }
        _inputField.text = null;
    }
}
