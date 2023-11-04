using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int LevelReached { get; set; }
    public int LevelInPlay { get; set; }

    GamePlayCanvas canvas;

    public GamePlayCanvas Canvas { get { return canvas; } set { canvas = value; } }

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        LevelInPlay = 1;
      //  PlayerPrefs.SetInt("LevelReached", 1);
        LevelReached = PlayerPrefs.GetInt("LevelReached");

        Debug.LogError(LevelInPlay);
    }

    public void EndLevel()
    {
        canvas.endLevelPanel.SetActive(true);
        canvas.nextLevelButton.onClick.AddListener(() => NextLevel());
    }

    public void NextLevel()
    {
        LevelInPlay++;
        if (PlayerPrefs.GetInt("LevelReached") < LevelInPlay)
        {
            PlayerPrefs.SetInt("LevelReached", LevelInPlay);
        }

        SceneManager.LoadScene(LevelInPlay);
    }
}
