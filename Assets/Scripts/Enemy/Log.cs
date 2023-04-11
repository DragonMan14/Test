using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkDistance();
    }

    public virtual void checkDistance()
    {
        float distance = Vector3.Distance(target.position, this.transform.position);
        bool playerInRadius = distance <= chaseRadius && distance > attackRadius;
        if (playerInRadius && currentState != EnemyState.stagger) {
            changeState(EnemyState.walk);
            animator.SetBool("awake", true);
            Vector3 temp = Vector3.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(temp);
            Vector3 change = target.position - this.transform.position;
            change.Normalize();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        } else if (distance > chaseRadius) {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
            animator.SetBool("awake", false);
        } else {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
        }
    }
}
