using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPatrol : Log
{

    void Start()
    {
        currentState = EnemyState.walk;
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetBool("awake", true);
        
    }

    public override void checkDistance()
    {
        float distance = Vector3.Distance(target.position, this.transform.position);
        bool playerInRadius = distance <= chaseRadius && distance > attackRadius;
        if (playerInRadius && currentState != EnemyState.stagger) {
            Vector3 temp = Vector3.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(temp);
            Vector3 change = target.position - this.transform.position;
            change.Normalize();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        } else if (distance > chaseRadius) {
            Vector3 change = patrolRoute();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        } else {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
        }
    }
}
