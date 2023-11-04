using UnityEngine;

public class SafeHouse : MonoBehaviour, IPickuple
{
    LevelManager lvlManagerl;

    private void Start()
    {
        lvlManagerl = FindObjectOfType<LevelManager>();
    }

    public void Excute(Player player)
    {
        player.gameObject.SetActive(false);
        lvlManagerl.playersReached++;
        lvlManagerl.CheckLevelEnd();
    }
}
