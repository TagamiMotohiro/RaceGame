using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultManeger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI time_Text;
    // Start is called before the first frame update
    void Start()
    {
        float time = RaceManeger.GetTime();
        int timemin = (int)time / 60;
        int timesec = (int)time % 60;
        time_Text.text = timemin.ToString() + ":" + timesec.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
