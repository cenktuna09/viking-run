using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Collect()
    {
        CanvasManager.instance.CreateCollectTxt(LevelManager.instance.GetActiveLevel().player.transform.position, Color.red, "- 5");
    }

}
