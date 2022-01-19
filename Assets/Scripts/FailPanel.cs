using DG.Tweening;
using ElephantSDK;
using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailPanel : MonoBehaviour
{
    public static FailPanel instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void OpenPanel()
    {
        Debug.Log("Level Failed");
        Elephant.LevelFailed((LevelManager.GetLevelID() + 1));

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, (LevelManager.GetLevelID() + 1).ToString()); 

        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void ClosePanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => {

        });
    }

    public void RestartButtonAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
