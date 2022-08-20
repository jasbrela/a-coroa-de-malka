using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    
    public delegate void OnOptionChosen(Dialogue dialogue);
    private OnOptionChosen _optionChosen;
    private Dialogue _nextDialogue;

    
    public void SetUp(DialogueOption data, OnOptionChosen optionChosen)
    {
        // Set up UI
        text.text = data.text;
        button.interactable = true;
        
        // Set up next dialogue
        _optionChosen = optionChosen;
        _nextDialogue = data.nextDialogue;
    }

    public void OnClick()
    {
        _optionChosen?.Invoke(_nextDialogue);
    }
}
