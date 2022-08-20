using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private Dialogue firstDialogue;
    
    [Space(10)] [Header("UI")]
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private GameObject mainTextParent;
    [SerializeField] private Image scenario;
    [SerializeField] private Image character;
    
    [Space(10)][Header("Options")]
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Transform optionsParent;

    private bool _hasOptions;

    private void Start()
    {
        SetUp(firstDialogue);
    }

    private void SetUp(Dialogue dialogue)
    {
        // Reset
        foreach (Transform child in optionsParent)
        {
            Destroy(child.gameObject);
        }

        mainTextParent.SetActive(dialogue.mainText.Length > 0);
        mainText.text = dialogue.mainText;
        // scenario.sprite = dialogue.scenario;
        // character.sprite = dialogue.character;

        _hasOptions = dialogue.options.Length > 0;
        if (!_hasOptions) return;
        
        foreach (DialogueOption o in dialogue.options)
        {
            Option option = Instantiate(optionPrefab, optionsParent).GetComponent<Option>();
            option.SetUp(o, SetUp);
        }
    }

}
