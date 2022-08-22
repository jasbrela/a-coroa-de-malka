using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private Dialogue firstDialogue;

    [Space(10)] [Header("Input")]
    [SerializeField] private PlayerInput input;

    [Space(10)] [Header("Main UI")]
    [SerializeField] private GameObject mainParent;
    [SerializeField] private DialogueLayout normalLayout;
    [SerializeField] private DialogueLayout fullscreenLayout;
    [SerializeField] private Image speedBtn;
    [SerializeField] private Image historicBtn;

    [Space(10)] [Header("Speech Bubbles")]
    [SerializeField] private SpeechBubble normal;
    [SerializeField] private SpeechBubble thinking;

    [Space(10)] [Header("Historic")]
    [SerializeField] private TextMeshProUGUI historic;
    [SerializeField] private GameObject historicParent;

    [Space(10)] [Header("Audio")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip alarm;

    private const float Fast = 0.05f;
    private const float Faster = 0.025f;
    private const float Fastest = 0.01f;
    private int _speedIndex = 1;
    private float _speed;

    private DialogueLayout _currentLayout;
    
    private bool _hasOptions;
    private bool _typingEnded;
    private Dialogue _currentDialogue;
    private Dialogue _nextDialogue;

    public void ToggleSpeed()
    {
        switch (_speedIndex)
        {
            case 1:
                _speed = Faster;
                _speedIndex++;
                break;
            case 2:
                _speed = Fastest;
                _speedIndex++;
                break;
            case 3:
                _speed = Fast;
                _speedIndex = 1;
                break;
        }
    }
    private void Start()
    {
        source.PlayOneShot(alarm);
        _speed = Fast;
        historic.text = "";
        CloseHistoric();
        SetUp(firstDialogue);

        input.actions[InputActions.NextDialogue.ToString()].performed += OnTrySkip;
        input.actions[InputActions.CloseHistoric.ToString()].performed += CloseHistoric;
    }

    private void SetUp(Dialogue dialogue)
    {
        // Reset options
        if (_currentDialogue != null && _currentDialogue.tone != Tone.Write)
        {
            foreach (Transform child in _currentLayout.GetOptionsParent())
            {
                Destroy(child.gameObject);
            }
        }
        
        _currentDialogue = dialogue;
        _typingEnded = false;
        
        UpdateHistoric(_currentDialogue.mainText, _currentDialogue.person);
        
        SetLayout(dialogue.tone);
        
        _currentLayout.GetPersonParent().SetActive(true);
        _currentLayout.GetPersonName().text = dialogue.person.ToString();
        speedBtn.color = _currentLayout.GetButtonColor();
        historicBtn.color = _currentLayout.GetButtonColor();
        _currentLayout.GetCrown().gameObject.SetActive(false);

        switch (dialogue.tone)
        {
            case Tone.Normal:

                _currentLayout.GetSpeechBubble().sprite = normal.background;
                _currentLayout.GetCrown().sprite = normal.crown;
                break;
            case Tone.Think:
                _currentLayout.GetSpeechBubble().sprite = thinking.background;
                _currentLayout.GetCrown().sprite = thinking.crown;
                break;
        }

        if (dialogue.scenario != null) _currentLayout.GetScenario().sprite = dialogue.scenario;
        if (dialogue.character != null) _currentLayout.GetCharacter().sprite = dialogue.character;

        _hasOptions = dialogue.options.Length > 0;
        _nextDialogue = !_hasOptions ? dialogue.nextDialogue : null;
        
        StartCoroutine(Type());
    }

    private void SetLayout(Tone tone)
    {
        if (tone == Tone.Write)
        {
            normalLayout.gameObject.SetActive(false);
            
            _currentLayout = fullscreenLayout;
            _currentLayout.gameObject.SetActive(true);
        }
        else
        {
            fullscreenLayout.gameObject.SetActive(false);
            
            _currentLayout = normalLayout;
            _currentLayout.gameObject.SetActive(true);
            _currentLayout.GetTextParent().SetActive(_currentDialogue.mainText.Length > 0);
        }
    }
    
    private IEnumerator Type()
    {
        _currentLayout.GetText().text = "";
        foreach (char c in _currentDialogue.mainText)
        {
            if (_typingEnded) break;
            _currentLayout.GetText().text += c;
            yield return new WaitForSeconds(_speed);
        }
        if (!_typingEnded) OnTypingEnded();
    }

    public void OnTrySkip()
    {
        if (!_typingEnded)
        {
            SkipTyping();
        } else if (!_hasOptions && _typingEnded)
        {
            if (_nextDialogue != null)
            {
                SetUp(_nextDialogue);
            }
            else
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
    
    private void OnTrySkip(InputAction.CallbackContext obj)
    {
        OnTrySkip();
    }

    private void SkipTyping()
    {
        _currentLayout.GetText().text = _currentDialogue.mainText;
        OnTypingEnded();
    }

    private void OnTypingEnded()
    {
        _typingEnded = true;
        StartCoroutine(CrownAnimation());
        if (_currentDialogue.tone != Tone.Write)
        {
            ShowOptions(_currentDialogue.options);
        }
    }

    private IEnumerator CrownAnimation()
    {
        while (_typingEnded)
        {
            _currentLayout.GetCrown().gameObject.SetActive(!_currentLayout.GetCrown().IsActive());
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void ShowOptions(DialogueOption[] options)
    {
        foreach (DialogueOption o in options)
        {
            Option option = Instantiate(_currentLayout.GetOptionPrefab(), _currentLayout.GetOptionsParent()).GetComponent<Option>();
            option.SetUp(o, OnOptionChosen);
        }
    }

    private void OnOptionChosen(Dialogue dialogue, string text)
    {
        source.PlayOneShot(click);
        UpdateHistoric(text, Person.Malka);
        SetUp(dialogue);
    }
    
    private void UpdateHistoric(string text, Person person)
    {
        if (person != Person.Narrative)
        {
            historic.text = historic.text + "\n\n" + "<b>" + GetAccent(person) + ":</b> " + text;
        }
        else
        {
            historic.text = historic.text + "\n\n" + "          " + text;
        }
    }

    private string GetAccent(Person person)
    {
        switch (person)
        {
            case Person.Julio:
                return "Júlio";
            case Person.Pamela:
                return "Pâmela";
            case Person.Malka:
                return "Malká";
            default:
                return "404";
        }
    }

    public void OpenHistoric()
    {
        input.SwitchCurrentActionMap("UI");
        mainParent.SetActive(false);
        historicParent.SetActive(true);
    }
    
    public void CloseHistoric()
    {
        mainParent.SetActive(true);
        historicParent.SetActive(false);
        input.SwitchCurrentActionMap("Dialogue");
    }
    
    private void CloseHistoric(InputAction.CallbackContext obj)
    {
        CloseHistoric();
    }

}
