using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractChest : Interactable
{
    public Inventory playerInventory;
    public Item contents;
    public bool isOpen;
    public ObjectSignals raiseItem;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestOpened();
            }
        }
    }

    public void OpenChest()
    {
        dialogueBox.SetActive(true);
        dialogueText.text = contents.itemDescription;
        playerInventory.addItem(contents);
        playerInventory.currentItem = contents;
        anim.SetBool("opened", true);
        raiseItem.Raise();
        isOpen = true;
        context.Raise();
    }

    public void ChestOpened()
    {
        playerInRange = false;
        dialogueBox.SetActive(false);
        raiseItem.Raise();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) 
        {
            context.Raise();
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) 
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
