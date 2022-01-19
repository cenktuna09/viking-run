using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class cloudScript : MonoBehaviour
{
    [SerializeField] public GameObject cloudRef;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(cloudRef.transform.position, 3f).SetEase(Ease.Linear).SetSpeedBased().SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        

        //transform.DOMove(cloudRef.transform.position, 2f).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
        //{
         //   transform.DOMove(cloudRef2.transform.position, 2f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);

        //});
    }
}
