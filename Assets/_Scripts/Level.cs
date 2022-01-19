using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public PlayerManager player;

    public GameObject[] allProps;

    public float totalDistance;
    public void CloseAllProps()
    {
        for (int i = 0; i < allProps.Length; i++)
        {
            allProps[i].SetActive(false);
        }
    }
}
