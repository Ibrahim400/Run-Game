using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPosition
{
    public Transform[] triggerSpawntPoints;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] int levelTriggers;
    [SerializeField] PlayerTrigger triggerPrefab;

    [HideInInspector] public int triggersTaken;

    public List<GameObject> players = new List<GameObject>();
    public List<SpawnPosition> spawnPositions = new List<SpawnPosition>();
    public int LevelTriggers { get { return levelTriggers; } }

    public int playersReached { get; set; }
    public int index { get; set; }
    public bool islevelEnd { get; set; }
    public int triggersTakenInside { get; set; }

    public int triggersInIndx 
    {
        get
        {
            return spawnPositions[index].triggerSpawntPoints.Length;
        }

        set
        {
           
        }
    }

    SafeHouse house;
    

    private void Start()
    {
        house = FindObjectOfType<SafeHouse>();

        if (levelTriggers > 0)
            CloneTriggers();

        CheckSafeHouseVisibility();           
    }

    public void CheckLevelEnd()
    {
        if (playersReached == players.Count)
        {
            GameManager.instance.EndLevel();
        }
        else
        {
            Debug.LogError("Level not end yet");
        }
    }

    public void RemovePlayer(GameObject player)
    {
        int index = players.FindIndex(x => x == player);

        if (index != -1)
        {
            GameObject go = players[index];
            players.RemoveAt(index);
            Destroy(go);
        }
    }

    public void CheckSafeHouseVisibility()
    {
        if (triggersTaken == levelTriggers)
        {
            house.gameObject.SetActive(true);
        }
        else
        {
            house.gameObject.SetActive(false);
        }
    }

    public void CloneTriggers()
    {
        for (int j = 0; j < spawnPositions[index].triggerSpawntPoints.Length; j++)
        {
            PlayerTrigger trgiger = Instantiate(triggerPrefab, spawnPositions[index].triggerSpawntPoints[j].position, Quaternion.identity);
        }
    }
}
