using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelsPanel;
    [SerializeField] Vector3 scale;
    [SerializeField] Transform slotsHolder;
    [SerializeField] LevelSlot[] slots;

    int slotsCount;

    private void Start()
    {
        slotsCount = slotsHolder.childCount;
        slots = new LevelSlot[slotsCount];

    }

    public void OnClick_EnablePanel()
    {
        levelsPanel.SetActive(true);
        AnimatePanel(levelsPanel, scale);
        OnLevelSelectorLoad();
    }

    void AnimatePanel(GameObject panel, Vector3 scale)
    {
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(scale, 0.3f);
    }

    void LoadLevels()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i + 1 <= GameManager.instance.LevelReached)
            {
                slots[i].IsLocked = false;
                slots[i].CheckLevelAvailability();
            }
            else
            {
                slots[i].IsLocked = true;
                slots[i].CheckLevelAvailability();
            }
        }
    }

    void OnLevelSelectorLoad()
    {
        InitializeSlots();
        LoadLevels();
    }

    void InitializeSlots()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            slots[i] = slotsHolder.GetChild(i).GetComponent<LevelSlot>();
        }
    }
}
