using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLayout : MonoBehaviour
{
    [SerializeField] private GameObject layout;
    [SerializeField] private Image speechBubble;
    [SerializeField] private Image crown;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject textParent;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Transform optionsParent;
    [SerializeField] private Image scenario;
    [SerializeField] private Image character;

    public GameObject GetLayout()
    {
        return layout;
    }

    public Image GetSpeechBubble()
    {
        return speechBubble;
    }
    
    public Image GetCrown()
    {
        return crown;
    }

    public TextMeshProUGUI GetText()
    {
        return text;
    }

    public GameObject GetTextParent()
    {
        return textParent;
    }

    public GameObject GetOptionPrefab()
    {
        return optionPrefab;
    }

    public Transform GetOptionsParent()
    {
        return optionsParent;
    }

    public Image GetScenario()
    {
        return scenario;
    }

    public Image GetCharacter()
    {
        return character;
    }
}
