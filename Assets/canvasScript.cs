using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class canvasScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         
        transform.position = new Vector3(
            player.transform.position.x,
            transform.position.y,
            transform.position.z);
        */

        //transform.DOMove(new Vector3(player.transform.position.x,transform.position.y,transform.position.z), 0f);

        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.position = new Vector3(player.transform.position.x,transform.position.y,transform.position.z);
    }


}
