using DG.Tweening;
using ElephantSDK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using MoreMountains.NiceVibrations;
using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;

public class CompletePanel : MonoBehaviour
{
    public static CompletePanel instance;

    [Header("Props")]
    public Button nextButton;
    public TextMeshProUGUI collectedAmount;

    [Header("Percentage")]
    private float percentage;
    public Image fillImage;
    public TextMeshProUGUI percentageText;

    [Header("Filled IMG")]
    public Image filledIMG;
    public GameObject[] touristIMGs;
    public GameObject imgRef;
    //public GameObject touristImgList;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        transform.localScale = Vector3.zero;

    }

    public void OpenPanel(int levelID)
    {
        Elephant.LevelCompleted((LevelManager.GetLevelID() + 1));
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, (LevelManager.GetLevelID() + 1).ToString());

        nextButton.enabled = true;
        collectedAmount.SetText("GET " + LevelManager.instance.GetActiveLevel().player.collectedTouristNO/3 + " " +
            "VIKING");
        //nextButton.transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, 0.5f).SetDelay(1).SetEase(Ease.OutBack).OnComplete(() => {

            //FillPercentages();
        });

        //glowIMG.rectTransform.DORotate(new Vector3(0, 0, 90), 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    public void ClosePanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => 
        {

            fillImage.transform.parent.transform.DOKill();
            fillImage.transform.parent.transform.localScale = Vector3.one;
            fillImage.fillAmount = 0;

            if (percentage == 1)
            {
                percentage = 0;

            }
        });
    }


    public void NextLevelButtonAction()
    {

        nextButton.enabled = false;
        ScaleTourIMGs();
    }

    public void LoadNewLevel()
    {
         
        //SceneManager.LoadScene(1);
        //Camera.main.transform.SetParent(null);
        LevelManager.LoadNextLevel();
        //Camera.main.transform.DOMove(new Vector3(0, 4.3f, -6.83f), 0f).SetEase(Ease.Linear);
        //Camera.main.transform.DORotate(new Vector3(18.167f, 0, 0), 0f).SetEase(Ease.Linear);
        //nextButton.enabled = false;
        for (int i = 0; i < touristIMGs.Length; i++)
        {
            touristIMGs[i].GetComponent<imgScript>().ComeBack();
        }
            
        ClosePanel();
    }
    public void ScaleTourIMGs()
    {
       
        for (int i = 0; i < touristIMGs.Length; i++)
        {
            touristIMGs[i].transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
        }


        StartCoroutine("MoveTourIMGs");
    }

    public IEnumerator MoveTourIMGs()
    {
       
        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < touristIMGs.Length; i++)
        {
            //touristIMGs[i].transform.SetParent(CanvasManager.instance.touristTokenIMG.transform.parent);
            touristIMGs[i].transform.DOMove(imgRef.transform.position, 0.4f).SetEase(Ease.Linear);
            touristIMGs[i].transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.Linear).OnComplete(() =>
            {
                
            });
        }

        Invoke("LoadNewLevel", 0.42f);
        MoneyManager.Increment(LevelManager.instance.GetActiveLevel().player.collectedTouristNO/3);
    }
}
