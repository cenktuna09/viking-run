using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using GameAnalyticsSDK;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    private int reactIndex = 0;

    [Header("GUI")]
    public TextMeshProUGUI levelTxt;
    public GameObject guide;
    public Slider progressSlider;
    public Image flashPhoto;
    public Image[] reactions;
    public Image touristTokenIMG;

    [Header("Tourist Point")]
    public TextMeshProUGUI touristCounter;

    [Header("UI Prefabs")]
    public TextMeshProUGUI collectValue;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameAnalytics.Initialize();
        Debug.Log("GameAnalytics initialized!");
    }

    public void GiveReactions()
    {
        reactions[reactIndex].gameObject.SetActive(true);

        reactions[reactIndex].rectTransform.DOAnchorPosY(800, 0.5f).SetEase(Ease.Flash);

        reactions[reactIndex].DOColor(new Color(1, 1, 1, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => {

            reactions[reactIndex].rectTransform.localPosition = new Vector3(0, 500, 0);
            reactions[reactIndex].gameObject.SetActive(false);
            reactions[reactIndex].DOColor(new Color(1, 1, 1, 1), 0f);

        });

        if (reactIndex < reactions.Length - 1)
        {
            reactIndex++;
        }
        else
        {
            reactIndex = 0;
        }
    }


    public void CreateCollectTxt(Vector3 pos,Color color,string amount)
    {
        var txt = Instantiate(collectValue, transform);

        txt.color = color;
        txt.text =amount;

        pos.y += 0.7f;
        pos.x += 1.2f;
        var convertedPos = Camera.main.WorldToScreenPoint(pos);
        txt.transform.position = convertedPos;

        txt.GetComponent<TextMeshProUGUI>().DOColor(new Color(1, 1, 1, 0), 1.2f);
        txt.transform.DOLocalMoveY(convertedPos.y + 200, 2f).SetEase(Ease.Linear);

        Destroy(txt.gameObject, 1f);

    }
}
