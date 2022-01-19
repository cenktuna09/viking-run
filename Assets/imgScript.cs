using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class imgScript : MonoBehaviour
{
    public Vector3 firstPos;
    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComeBack()
    {
        transform.DOMove(firstPos, 0f).SetEase(Ease.Linear);
    }
}
