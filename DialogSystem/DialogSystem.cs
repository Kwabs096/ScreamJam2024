using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogTab;

    [SerializeField] private string[] _englishTexts, _portugueseTexts;
    private List<string[]> _allTexts = new List<string[]>();
    [SerializeField] private TextMeshProUGUI _dialogText;
    private string[] _currentTextLanguage;

    [SerializeField] string[] _dictionaryNames;
    Dictionary<string, int> _namesDictionary = new Dictionary<string, int>();
    [SerializeField] string[] _englishNames, _portugueseNames;
    private List<string[]> _allNames = new List<string[]>();
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] Sprite[] _charactersSprites;
    [SerializeField] Image _characterIcon;
    private string[] _currentNameLanguage;

    [SerializeField] private float _delayLetter = 0.05f;
    private int _usedLanguage = 0;
    void Start()
    {
        _allTexts.Add(_englishTexts);
        _allTexts.Add(_portugueseTexts); // able to add other languages
        _allNames.Add(_englishNames);
        _allNames.Add(_portugueseNames);
        _usedLanguage = PlayerPrefs.GetInt("language", _usedLanguage);
        _currentTextLanguage = _allTexts[_usedLanguage];
        _currentNameLanguage = _allNames[_usedLanguage];

        for(int i = 0; i < _dictionaryNames.Length; i++)
        {
            _namesDictionary.Add(_dictionaryNames[i], i);
        }
    }
    public void PrintDialog(int indexText, string name) => StartCoroutine(ShowText(indexText, name));
    IEnumerator ShowText(int indexText, string name)
    {
        dialogTab.SetActive(true);
        if (_namesDictionary.ContainsKey(name))
        {
            int nameId = _namesDictionary[name];
            _nameText.text = _currentNameLanguage[nameId];
            _characterIcon.sprite = _charactersSprites[nameId];
        }
        else
        {
            Debug.Log("Incorrect name");
        }
        string currentText = "";
        string fullText = _allTexts[_usedLanguage][indexText];
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            _dialogText.text = currentText;
            yield return new WaitForSeconds(_delayLetter);
        }
    }
    public void ChangeLanguage(int indexLanguage)
    {
        _usedLanguage = indexLanguage;
        _currentTextLanguage = _allTexts[_usedLanguage];
        _currentNameLanguage = _allNames[_usedLanguage];
        PlayerPrefs.SetInt("language", _usedLanguage);
    }
    public void CloseTab()
    {
        dialogTab.SetActive(false);
    }
}
