using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LokiScript : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;
    private int waypointIndex;
    private float dist;
    private bool isAttacked;
    //[SerializeField] public GameObject player;
    private Animator animator;
    private Transform Player;
    void Start()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if(dist < 1f)
        {
            IncreaseIndex();
        }
        if(!isAttacked)
        {
            Patrol();
        }
        else
        {
            transform.LookAt(Player);
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex]);
    }

    public void LokiAttack()
    {
        isAttacked = true;
        animator.SetBool("Attack", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player = other.transform;
            //transform.LookAt(other.transform);
        }
    }
}
