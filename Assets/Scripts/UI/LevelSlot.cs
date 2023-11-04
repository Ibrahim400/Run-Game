using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSlot : MonoBehaviour
{
    public bool IsLocked { get { return isLocked; } set { isLocked = value; } }

    public int levelNumber;

    bool isLocked;
    Button btn;
    Text numText;

    private void OnEnable()
    {
        btn = GetComponent<Button>();
        numText = GetComponentInChildren<Text>();
        btn.onClick.AddListener(() => SetManagerValues());
        
    }

    private void Start()
    {
        numText.text = levelNumber.ToString();
    }
    public void CheckLevelAvailability()
    {
        btn.interactable = (!isLocked) ? true : false;
    }

    public void SetManagerValues()
    {
        GameManager.instance.LevelInPlay = levelNumber;
        SceneManager.LoadScene(levelNumber);
    }
}
