using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public Sprite portraitImage;

    [TextArea(2, 4)]
    public string dialogueText;
}