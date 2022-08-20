using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum InputActions
{
    NextDialogue
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private Dialogue firstDialogue;
    [Range(0.0001f, 1f)][SerializeField] private float speed = 0.01f;

    [Space(10)] [Header("Input")]
    [SerializeField] private PlayerInput input;

    [Space(10)] [Header("Main UI")]
    [SerializeField] private GameObject mainParent;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private GameObject mainTextParent;
    [SerializeField] private Image scenario;
    [SerializeField] private Image character;

    [Space(10)] [Header("Historic")]
    [SerializeField] private TextMeshProUGUI historic;
    [SerializeField] private GameObject historicParent;

    [Space(10)][Header("Options")]
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Transform optionsParent;
    
    private bool _hasOptions;
    private bool _dialogueEnded;
    private Dialogue _currentDialogue;
    private Dialogue _nextDialogue;

    private void Start()
    {
        CloseHistoric();
        SetUp(firstDialogue);
        input.actions[InputActions.NextDialogue.ToString()].performed += OnTrySkip;
    }

    private void OnTrySkip(InputAction.CallbackContext obj)
    {
        if (!_dialogueEnded)
        {
            SkipTyping();
        } else if (!_hasOptions && _dialogueEnded)
        {
            SetUp(_nextDialogue);
        }
    }

    private void SkipTyping()
    {
        mainText.text = _currentDialogue.mainText;
        OnDialogueEnded();
    }

    private IEnumerator Type()
    {
        mainText.text = "";
        foreach (char c in _currentDialogue.mainText)
        {
            if (_dialogueEnded) break;
            mainText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    private void OnDialogueEnded()
    {
        _dialogueEnded = true;
        ShowOptions(_currentDialogue.options);
    }
    
    private void SetUp(Dialogue dialogue)
    {
        _currentDialogue = dialogue;
        _dialogueEnded = false;
        
        // Reset
        foreach (Transform child in optionsParent)
        {
            Destroy(child.gameObject);
        }

        mainTextParent.SetActive(dialogue.mainText.Length > 0);

        StartCoroutine(Type()); //mainText.text = dialogue.mainText;

        UpdateHistoric(dialogue.mainText);
        
        if (dialogue.scenario != null) scenario.sprite = dialogue.scenario;
        if (dialogue.character != null) character.sprite = dialogue.character;

        _hasOptions = dialogue.options.Length > 0;
        _nextDialogue = !_hasOptions ? dialogue.nextDialogue : null;
    }

    private void ShowOptions(DialogueOption[] options)
    {
        foreach (DialogueOption o in options)
        {
            Option option = Instantiate(optionPrefab, optionsParent).GetComponent<Option>();
            option.SetUp(o, OnOptionChosen);
        }
    }

    private void OnOptionChosen(Dialogue dialogue, string text)
    {
        UpdateHistoric(text);
        SetUp(dialogue);
    }

    private void UpdateHistoric(string text)
    {
        historic.text = text + "\n\n" + historic.text;
    }

    public void OpenHistoric()
    {
        mainParent.SetActive(false);
        historicParent.SetActive(true);
    }

    public void CloseHistoric()
    {
        mainParent.SetActive(true);
        historicParent.SetActive(false);
    }
    

}
