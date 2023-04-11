using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractNPC : InteractableDialogue
{
    public List<Quest> questList;

    void Start()
    {
        if (questList.Count > 0)
        {
            UpdateDialogueState();
        }
        else
        {
            currentState = dialogueState.defaultDialogue;
        }
    }
    void Update() 
    {
        if (questList.Count > 0)
        {
            UpdateDialogueState();
        }
        playDialogue();
    }

    void UpdateDialogueState()
    {
        if (!questList[0].questActive)
        {
            currentState = dialogueState.preQuest;
        }
        else if (questList[0].questActive)
        {
            if (!questList[0].isDone())
            {
                currentState = dialogueState.postQuest;
            }
            else
            {
                currentState = dialogueState.finishedQuest;
            }
        }

        if (currentState == dialogueState.preQuest && dialogueIndex >= dialogue.preAcceptQuest.Count)
        {
            questList[0].startQuest();
        }

        if (currentState == dialogueState.finishedQuest && dialogueIndex >= dialogue.finishedQuest.Count)
        {
            questList.RemoveAt(0);            
        }
    }
}
