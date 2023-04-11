using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableDialogue : Interactable
{
    public enum dialogueState {
        defaultDialogue,
        preQuest,
        postQuest,
        finishedQuest
    }

    public dialogueState currentState;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public DialogueList dialogue;
    public int dialogueIndex = 0;
    
    public void playDialogue() {
        if (!playerInRange)
        {
            return;
        }

        if (Input.GetKeyDown("space")) {
            if (!dialogueBox.activeInHierarchy) 
            {
                progressDialogue();
                openDialogueUI();
            } 
            else 
            {
                progressDialogue();
            }
        } 
    }

    void openDialogueUI() 
    {
        dialogueBox.SetActive(true);
    }

    void closeDialogueUI() 
    {
        dialogueIndex = 0;
        dialogueBox.SetActive(false);
    }

    void progressDialogue()
     {
        if (currentState == dialogueState.defaultDialogue)
        {
            if (dialogueIndex < dialogue.defaultDialogue.Count) 
            {
                dialogueText.text = dialogue.defaultDialogue[dialogueIndex++];
            }
            else 
            {
                closeDialogueUI();
            }
        }
        else if (currentState == dialogueState.preQuest)
        {
            if (dialogueIndex < dialogue.preAcceptQuest.Count) 
            {
                dialogueText.text = dialogue.preAcceptQuest[dialogueIndex++];
            }
            else 
            {
                closeDialogueUI();
            }
        }
        else if (currentState == dialogueState.postQuest)
        {
            if (dialogueIndex < dialogue.postAcceptQuest.Count) 
            {
                dialogueText.text = dialogue.postAcceptQuest[dialogueIndex++];
            }
            else 
            {
                closeDialogueUI();
            }
        }
        else if (currentState == dialogueState.finishedQuest)
        {
            if (dialogueIndex < dialogue.finishedQuest.Count) 
            {
                dialogueText.text = dialogue.finishedQuest[dialogueIndex++];
            }
            else 
            {
                closeDialogueUI();
                currentState = dialogueState.defaultDialogue;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            context.Raise();
            playerInRange = false;
            closeDialogueUI();
        }
    }
}
