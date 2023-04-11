using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKillEnemies : MonoBehaviour
{
    public bool active;
    public bool completed;
    public List<GameObject> doors;
    public KillCount enemiesKilled;
    private int initialKillCount;
    public int amountToKill;


    // Update is called once per frame
    void Update()
    {
        if (active && isDone())
        {
            endKillTrial();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || completed)
        {
            return;
        }
        initialKillCount = enemiesKilled.logsKilled;
        active = true;
        startKillTrial();
    }

    void startKillTrial()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().Close();
        }
    }

    void endKillTrial()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().Open();
        }
        completed = true;
    }

    public bool isDone()
    {
        return enemiesKilled.logsKilled - initialKillCount >= amountToKill;     
    }
}
