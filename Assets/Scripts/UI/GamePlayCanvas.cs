using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GamePlayCanvas : MonoBehaviour
{
    public int directorsNum;

    [SerializeField] GameObject pausePanel;
    [SerializeField] Text directorNumber;

    public GameObject endLevelPanel;
    public Button nextLevelButton;

    List<Transform> butns = new List<Transform>();
    List<Sequence> seqs = new List<Sequence>();

    private void Start()
    {
        directorsNum = Mathf.Clamp(directorsNum, 0, 100);
        directorNumber.text = ObjectPool.instance.items.Find(x => x.prefab.tag == "Director").Ammount.ToString();
        GameManager.instance.Canvas = this;
        directorNumber.text = directorsNum.ToString();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void UpdateDirectorsText()
    {
        directorNumber.text = directorsNum.ToString();
    }

    public void Pause()
    {
        StartCoroutine(Pausing());        
    }

    IEnumerator Pausing()
    {
        pausePanel.SetActive(true);
        pausePanel.transform.localScale = Vector3.zero;

        pausePanel.transform.DOScale(0.7f, 0.3f);

        yield return new WaitForSeconds(0.4f);

        Time.timeScale = 0;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void AnimateButtons()
    {
        for (int i = 0; i < butns.Count; i++)
        {
            if (seqs.Count >= i)
            {
                seqs[i] = DOTween.Sequence();
            }
            else if (seqs[i].IsPlaying())
            {
                seqs[i].Kill();
            }          
        }
    }
}
