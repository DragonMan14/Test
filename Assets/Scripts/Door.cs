using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DoorType {
    enemy,
    key,
    button
}

public class Door : InteractableDialogue
{
    public DoorType type;
    public bool isOpen;
    public Inventory playerInventory;
    public BoxCollider2D physicsCollider;
    public SpriteRenderer doorSprite;

    public Sprite openSprite;
    public Sprite closedSprite;


    void Start() 
    {
        if (isOpen)
        {
            Open();
        }
        else 
        {
            Close();
        }
    }
    
    void Update()
    {
        if(!playerInRange)
        {
            return;
        }
        if (Input.GetKeyDown("space"))
        {
            if (playerInRange && type == DoorType.key)
            {
                if (playerInventory.HasKeys())
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
                else {
                    playDialogue();
                }
            }
        }
    }   
    
    public void Open()
    {
        doorSprite.sprite = openSprite;
        isOpen = true;
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        doorSprite.sprite = closedSprite;
        isOpen = false;
        physicsCollider.enabled = true;
    }

}
