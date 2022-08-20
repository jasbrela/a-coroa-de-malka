using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogue/Option", fileName = "new Option")]
public class DialogueOption : ScriptableObject
{
    [TextArea(1, 3)] public string text;
    [Space(5)]
    public Dialogue nextDialogue;
}
