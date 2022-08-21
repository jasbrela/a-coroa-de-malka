using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    
    public delegate void OnOptionChosen(Dialogue dialogue, string text);
    private OnOptionChosen _optionChosen;
    
    private DialogueOption _currentData;
    private Dialogue _nextDialogue;

    public void SetUp(DialogueOption data, OnOptionChosen optionChosen)
    {
        // Set up data
        _currentData = data;
        
        // Set up UI
        text.text = _currentData.text;
        button.interactable = true;

        // Set up next dialogue
        _optionChosen = optionChosen;
        _nextDialogue = data.nextDialogue;
    }

    public void OnClick()
    {
        string temp = "<i>" + _currentData.text + "</i>";
        _optionChosen?.Invoke(_nextDialogue, temp);
    }
}
