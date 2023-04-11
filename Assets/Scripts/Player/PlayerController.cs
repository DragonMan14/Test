using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact,
    stagger
}

public enum PlayerDirection {
    up,
    down,
    left,
    right
}

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    
    [SerializeField]
    private float speed = 5;

    private Vector3 change;
    private Animator animator;

    public PlayerState currentState;

    public FloatValue currentHealth;
    public ObjectSignals playerHealthSignal;

    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = startingPosition.runtimeValue;

        switch (startingPosition.facing)
        {
            case PlayerDirection.down:
                animator.SetFloat("moveY", -1);
                break;
            case PlayerDirection.up:
                animator.SetFloat("moveY", 1);
                break;
            case PlayerDirection.right:
                animator.SetFloat("moveX", 1);
                break;
            case PlayerDirection.left:
                animator.SetFloat("moveX", -1);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == PlayerState.interact) {
            return;
        }
        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        change.Normalize();
        // If input is detected
        if (currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        } 
    }

    void Update() {
        if (currentState == PlayerState.interact) {
            return;
        }
        // Attack animation
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());
        }
    }

    void UpdateAnimationAndMove() {
        if (change != Vector3.zero) {
            // Set the correct animation
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            // Move the character
            MoveCharacter();
            if(change.x > 0)
                {
                    startingPosition.facing = PlayerDirection.right;
                }
                else if(change.x < 0)
                {
                    startingPosition.facing = PlayerDirection.left;
                }
                if(change.y > 0)
                {
                    startingPosition.facing = PlayerDirection.up;
                }
                else if (change.y < 0)
                {
                    startingPosition.facing = PlayerDirection.down;
                }
        } else {
            animator.SetBool("moving", false);
        }
    }

    // Moves the character
    void MoveCharacter() {
        rb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
    
    private IEnumerator AttackCo() {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.4f);
        if (currentState != PlayerState.interact) {
            currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem == null) {
            return;
        }
        if (currentState != PlayerState.interact)
        {
            animator.SetBool("itemReceived", true);
            currentState = PlayerState.interact;
            receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
        }
        else
        {
            animator.SetBool("itemReceived", false);
            currentState = PlayerState.walk;
            playerInventory.currentItem = null;
            receivedItemSprite.sprite = null;
        }
    }

    public void knock(float knockTime, float damage) {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0) 
        {
            StartCoroutine(knockCo(knockTime));
        } 
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator knockCo(float knockTime) {
        yield return new WaitForSeconds(knockTime);
        rb.velocity = Vector2.zero;
        currentState = PlayerState.walk;
    }
}


