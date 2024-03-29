using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue", fileName = "new Dialogue")]
public class Dialogue : ScriptableObject
{
    public Person person;
    public Sprite character;
    public Sprite scenario;
    [TextArea(1, 5)] public string mainText;
    public DialogueOption[] options;
    public Tone tone;
    
    [Header("if NO options, please insert the next dialogue")]
    public Dialogue nextDialogue;
}

public enum Person
{
    Malka,
    Julio,
    Pamela,
    Narrative
}