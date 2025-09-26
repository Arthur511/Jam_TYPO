using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;

    public int ScoreToReach;
    [HideInInspector] public int _placementToComplete;

    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] List<SpawnableObject> _spawnableObjects;
    [SerializeField] Letters _lettersForLevel;

    [SerializeField] Transform _spawnPointObject;

    HashSet<string> _alreadySpawnables = new HashSet<string>();

    bool _isInList = true;
    bool _isTextVisible = false;

    float _timer = 3f;
    float _currentTimer;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (_isTextVisible)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= 0)
            {
                DispearedText();
                _isTextVisible = false;
            }
        }
    }


    public void OnSubmit()
    {
        _isInList = true;
        string _lowerInput = _inputField.text.ToLower().Trim();
        for (int i = 0; i < _lowerInput.Length; i++)
        {
            if (!_lettersForLevel._levelLetters.Contains(_lowerInput[i]))
            {
                _isInList = false;
                AppearedText("Utilisation d'une lettre inexistante dans ce niveau");
                _currentTimer = _timer;
                _isTextVisible = true;
                break;
            }
        }

        if (_alreadySpawnables.Contains(_lowerInput))
        {
            _inputField.text = null;
            AppearedText("Mot déjà utilisé !");
            _currentTimer = _timer;
            _isTextVisible = true;
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


    void AppearedText(string sentence)
    {
        _textMeshPro.text = sentence;
        _textMeshPro.DOFade(1, 1f);
    }

    void DispearedText()
    {
        _textMeshPro.DOFade(0, 1f);
    }

    public void WinRoom()
    {
        Debug.Log("Room Completed");
    }

}
