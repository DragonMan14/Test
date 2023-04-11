using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quests/Kill Quest")]
public class KillQuest : Quest
{
    
    public enum type {
        Log
    }

    public KillCount enemiesKilled;
    private int initialEnemyKillCount;
    public int enemyAmountToKill;
    public type enemyType;

    public override void startQuest()
    {
        questActive = true;
        if (enemyType == type.Log)
        {
            initialEnemyKillCount = enemiesKilled.logsKilled;
        }
    }

    public override bool isDone()
    {
        if (enemyType == type.Log)
        {
            return enemiesKilled.logsKilled - initialEnemyKillCount >= enemyAmountToKill; 
        }
        return false;
    }
}
