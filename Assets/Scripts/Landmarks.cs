using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Landmarks : MonoBehaviour
{
    public GameObject cameraIMG;

    void Start()
    {
        var pos =cameraIMG.transform.localPosition;
        pos.y += 0.5f;
        cameraIMG.transform.DOLocalMoveY(pos.y, 1f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        cameraIMG.transform.DOKill();
    }
}
