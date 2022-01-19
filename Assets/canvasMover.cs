using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class canvasMover : MonoBehaviour
{

    public void GoUp()
    {
        transform.DOMoveY(0f, 0.1f).SetEase(Ease.Linear);
    }
    public void GoDown()
    {
        transform.DOMoveY(-0.045f, 0.1f).SetEase(Ease.Linear);
    }
}
