using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;

    public int ScoreToReach;
    [HideInInspector] public int _placementToComplete;

    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] List<SpawnableObject> _spawnableObjects;
    [SerializeField] Letters _lettersForLevel;

    [SerializeField] RectTransform parentRect;
    [SerializeField] GameObject _letterPrefab;
    [SerializeField] GameObject _grid;

    [SerializeField] Transform _spawnPointObject;

    HashSet<string> _alreadySpawnables = new HashSet<string>();

    bool _isInList = true;
    bool _isTextVisible = false;
    bool _isntExist = false;

    float _timer = 3f;
    float _currentTimer;
    private Scene _dontDestroyScene;

    private void Awake()
    {
        instance = this;

        foreach (char c in _lettersForLevel._levelLetters)
        {
            //int rows = Mathf.CeilToInt((float)_lettersForLevel._levelLetters.Count);
            //float cellHeight = parentRect.rect.height / rows ;
            //_grid.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellHeight, cellHeight);
            /*GameObject letter = Instantiate(_letterPrefab, _grid.transform);
            letter.GetComponent<TextMeshProUGUI>().text = c.ToString().ToUpper();*/
        }

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
        _inputField.text = null;
        for (int i = 0; i < _lowerInput.Length; i++)
        {
            if (!_lettersForLevel._levelLetters.Contains(_lowerInput[i]))
            {
                _isInList = false;
                //_inputField.text = null;
                AppearedText("Utilisation d'une lettre inexistante dans ce niveau");
                _currentTimer = _timer;
                _isTextVisible = true;
                return;
            }

        }

        if (_alreadySpawnables.Contains(_lowerInput))
        {
            //_inputField.text = null;
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
                    //_inputField.text = null;
                    return;
                }
                _isntExist = true;
            }
        }

        if (_isntExist)
        {
            //_inputField.text = null;
            AppearedText("Mot correct mais inexistant dans ce niveau");
            _currentTimer = _timer;
            _isTextVisible = true;
        }

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

    public void WinRoom(AudioSource audioSource, AudioClip clip)
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            GameObject temp = new GameObject("Temp");
            DontDestroyOnLoad(temp);
            _dontDestroyScene = temp.scene;
            Destroy(temp);


            if (_dontDestroyScene.IsValid())
            {
                foreach (GameObject go in _dontDestroyScene.GetRootGameObjects())
                {
                    Object.Destroy(go);
                }
            }

            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(WaitWinRoom(audioSource, clip));
        }
    }

    IEnumerator WaitWinRoom(AudioSource audioSource, AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        AppearedText("Pièce rangée !!!");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
