using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chage_Emission : MonoBehaviour
{
    Material myMat;
    AirRideMove moveScript;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = this.GetComponent<AirRideMove>();
        myMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (myMat.HasProperty("_EmissionColor"))
        {
            float c;
            c = (moveScript.GetCharge_Tank() / moveScript.GetMaxCharge())/2;
            myMat.SetColor("_EmissionColor",new Color(c,c,c));
        }
        else
        {
            Debug.Log("プロパティの取得に失敗");
        }
    }
}
