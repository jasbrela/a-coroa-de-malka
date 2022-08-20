using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue", fileName = "new Dialogue")]
public class Dialogue : ScriptableObject
{
    public Sprite character;
    public Sprite scenario;
    [TextArea(1, 5)] public string mainText;
    public DialogueOption[] options;

    [Header("if NO options, please insert the next dialogue")]
    public Dialogue nextDialogue;
}
