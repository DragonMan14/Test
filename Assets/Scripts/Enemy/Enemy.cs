using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    walk,
    attacking,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    
    public KillCount enemiesKilled;

    public List<GameObject> patrolPoints;
    public int currentPatrolPointIndex;
    
    public GameObject deathParticles;
    public float deathParticlesTime = 0.33f;

    // Start is called before the first frame update
    private void Awake()
    {
        health = maxHealth.runtimeValue;
    }

    public void changeState(EnemyState newState) 
    {
        if (currentState != newState) {
            currentState = newState;
        }
    }

    public void knock(Rigidbody2D objectHit, float knockTime, float damage) 
    {
        StartCoroutine(knockCo(objectHit, knockTime));
        takeDamage(damage);
    }

    public IEnumerator knockCo(Rigidbody2D objectHit, float knockTime) 
    {
        if (objectHit == null) {
            yield break;
        }
        yield return new WaitForSeconds(knockTime);
        objectHit.velocity = Vector2.zero;
        currentState = EnemyState.idle;
    }

    private void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {
            incrementKillCount();
            Destroy(Instantiate(deathParticles, this.transform.position, Quaternion.identity), deathParticlesTime);
            this.gameObject.SetActive(false);
        } 
    }

    private void incrementKillCount() {
        if (this.gameObject.HasTag("Log")) {
            enemiesKilled.logsKilled++;
        }
    }
    public Vector3 patrolRoute()
    {
        int nextPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
        GameObject nextPatrolPoint = patrolPoints[nextPatrolPointIndex];
        Vector3 temp = Vector3.MoveTowards(this.transform.position, nextPatrolPoint.transform.position, moveSpeed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        float distanceToNext = Vector3.Distance(this.transform.position, nextPatrolPoint.transform.position);
        if (distanceToNext < .01f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
        }
        return nextPatrolPoint.transform.position - this.transform.position;
    }
}
