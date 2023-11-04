using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour, IPickuple
{
    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void Excute(Player player)
    {
        if (levelManager.triggersTakenInside < levelManager.triggersInIndx)
        {
            levelManager.triggersTakenInside++;
            Destroy(gameObject);
        }

        if (levelManager.triggersTakenInside == levelManager.triggersInIndx)
        {
            levelManager.triggersTakenInside = 0;

            levelManager.triggersTaken++;

            if (levelManager.index < levelManager.spawnPositions.Count - 1)
            {
                levelManager.index++;
                levelManager.CloneTriggers();
            }
            
            levelManager.triggersInIndx = levelManager.spawnPositions[levelManager.index].triggerSpawntPoints.Length;

            levelManager.CheckSafeHouseVisibility();
            Destroy(gameObject);
        }       
    }
}
