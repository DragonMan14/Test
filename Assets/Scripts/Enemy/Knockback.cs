using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("enemyHitBox") && gameObject.CompareTag("attackHitBox")) {
            GameObject enemy = other.transform.parent.gameObject;
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb == null) {
                return;
            }
            Vector2 difference = (Vector2) (rb.transform.position - this.transform.position);
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
            enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
            enemy.GetComponent<Enemy>().knock(rb, knockTime, damage);
        }

        if (other.CompareTag("playerHurtBox") && gameObject.CompareTag("enemyHitBox")) {
            GameObject player = other.transform.parent.gameObject;
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc.currentState == PlayerState.stagger) {
                return;
            }
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector2 difference = (Vector2) (player.transform.position - this.transform.position);
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
            pc.currentState = PlayerState.stagger;
            pc.knock(knockTime, damage);
        }
    } 
}
