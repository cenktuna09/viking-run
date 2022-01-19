using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thief : MonoBehaviour
{
    private Animator animator;
    private Transform target;

    public bool steal = false;

    public int touristValue;
    void Start()
    {
        animator = GetComponent<Animator>();

        target = LevelManager.instance.GetActiveLevel().player.transform;
    }

    void Update()
    {
        if (steal)
            return;

        var dist = Vector3.Distance(transform.position, target.position);

        if (dist < 10f)
        {
            transform.DOLookAt(target.position, 0.1f,AxisConstraint.Y).SetEase(Ease.Linear);

            animator.SetBool("Walk", true);

            transform.parent.SetParent(target);

            transform.parent.DOLocalMove(new Vector3(0.2f, transform.localPosition.y, 1), 2).SetSpeedBased().OnComplete(()=>{

                animator.SetTrigger("Steal");
                steal = true;
                transform.parent.DOKill();
                transform.parent.SetParent(LevelManager.instance.GetActiveLevel().transform);
               
            });
        }
    }

}
