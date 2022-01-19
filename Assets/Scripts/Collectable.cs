using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int touristValue;
    
    public void Collect()
    {
        CanvasManager.instance.CreateCollectTxt(LevelManager.instance.GetActiveLevel().player.transform.position,Color.green,"+"+touristValue);
    }
}
