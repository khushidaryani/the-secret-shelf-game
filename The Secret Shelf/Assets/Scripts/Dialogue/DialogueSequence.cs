using UnityEngine;

// Scriptable Object
[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Dialogue/Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    public DialogueLine[] lines;
}