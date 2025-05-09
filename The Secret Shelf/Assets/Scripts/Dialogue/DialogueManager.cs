using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;

    [Header("Hint Button")]
    public Button hintButton;

    [Header("Typing Settings")]
    public float typingSpeed = 0.10f;

    [Header("Librarian Movement")]
    public LibrarianMovement librarianMovement;

    private DialogueLine[] lines;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;

    // Store the last line shown to reuse for hint display
    public static DialogueLine lastLine;

    public void StartDialogue(DialogueLine[] dialogueLines)
    {
        lines = dialogueLines;
        currentLineIndex = 0;
        isDialogueActive = true;
        isTyping = false;

        dialoguePanel.SetActive(true);
        if (hintButton != null)
            hintButton.gameObject.SetActive(false);

        if (librarianMovement != null)
            librarianMovement.SetDialogueActive(true);

        ShowCurrentLine();
    }
    private void Update()
    {
        if (!isDialogueActive || !Input.GetKeyDown(KeyCode.Space))
            return;

        if (isTyping)
        {
            // Skip typing animation if player presses space
            StopAllCoroutines();
            dialogueText.text = lines[currentLineIndex].dialogueText;
            isTyping = false;
        }
        else
        {
            ShowNextLine();
        }
    }

    // Display the current dialogue line with typing animation
    private void ShowCurrentLine()
    {
        if (currentLineIndex >= lines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines[currentLineIndex];
        characterName.text = line.characterName;
        if (line.portraitImage != null)
            portraitImage.sprite = line.portraitImage;

        // Save the last displayed line for hint button
        lastLine = line;
        StartCoroutine(TypeSentence(line.dialogueText));
    }

    // Coroutine that types out the sentence letter by letter
    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // Move to the next line in the sequence
    private void ShowNextLine()
    {
        currentLineIndex++;
        ShowCurrentLine();
    }

    // End the current dialogue +
    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        if (hintButton != null)
            hintButton.gameObject.SetActive(true);

        if (librarianMovement != null)
            librarianMovement.SetDialogueActive(false);
    }

    // Triggered when the hint button is clicked
    public void OnHintButtonClicked()
    {
        if (lastLine == null)
            return;

        bool isPanelOpen = dialoguePanel.activeSelf;
        dialoguePanel.SetActive(!isPanelOpen); // Toggles to show the last line of dialogue

        if (!isPanelOpen)
        {
            characterName.text = lastLine.characterName;
            portraitImage.sprite = lastLine.portraitImage;
            dialogueText.text = lastLine.dialogueText;

            isDialogueActive = false;
            isTyping = false;
        }

        if (hintButton != null)
            hintButton.gameObject.SetActive(true);
    }
}