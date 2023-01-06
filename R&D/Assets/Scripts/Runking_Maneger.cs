using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Runking_Maneger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var timescore = RaceManeger.Get_Goal_Time();
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timescore);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
