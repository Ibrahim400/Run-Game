using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SpawnPointPrototype
{
    public GameObject[] playerPrefab;
    public Transform spawnPoint;
}


public class PlayersGenerator : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<SpawnPointPrototype> protos = new List<SpawnPointPrototype>();
    [SerializeField] GameObject playerPrefab;
    [SerializeField] int spawnsNumber;
    [SerializeField] float spawnTime;

    public enum CloneType {MULTIPOSWITHTIME,MULTIPOSWITHOUTTIME,SINGLEPOS };
    public CloneType type;

    int index;
    int childIndex;
    float timer;
    int spawnedPlayersNum;
    LevelManager lvlManager;

    private void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();
        timer = spawnTime;
    }

    private void Update()
    {
        if (spawnedPlayersNum >= spawnsNumber)
        {
            Destroy(gameObject);
            return;
        } 


        switch (type)
        {
            case CloneType.SINGLEPOS:
                SingleClone();
                break;
            case CloneType.MULTIPOSWITHOUTTIME:
                MultiClone(false);
                break;
            case CloneType.MULTIPOSWITHTIME:
                MultiClone(true);
                break;
        }     
    }

  
    void SingleClone()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            GameObject pl = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            lvlManager.players.Add(pl);
            spawnedPlayersNum++;

            timer = 0;
        }
    }

    void MultiClone(bool isWithTime)
    {
        if (isWithTime)
        {
            timer += Time.deltaTime;

            if (timer >= spawnTime)
            {
                GameObject pls = Instantiate(protos[index].playerPrefab[childIndex], protos[index].spawnPoint.position, Quaternion.identity);
                lvlManager.players.Add(pls);
                childIndex++;
                spawnedPlayersNum++;

                if (childIndex == protos[index].playerPrefab.Length - 1 && index != protos.Count - 1) index++;
                timer = 0;
            }
        }
        else
        {
            for (int i = 0; i < protos.Count; i++)
            {

                for (int j = 0; j < protos[i].playerPrefab.Length; j++)
                {
                    GameObject pls = Instantiate(protos[i].playerPrefab[j], protos[i].spawnPoint.position, Quaternion.identity);
                    lvlManager.players.Add(pls);
                    spawnedPlayersNum++;
                }

            }
        }
    }
}
