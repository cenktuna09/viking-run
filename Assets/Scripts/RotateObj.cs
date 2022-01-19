using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObj : MonoBehaviour
{
    public Vector3 rotateVector;
    void Start()
    {
        transform.DOLocalRotate(rotateVector, 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
