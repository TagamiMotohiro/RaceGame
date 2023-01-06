using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSlider : MonoBehaviour
{
    public AirRideMove moveScript;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //チャージタンク内の値を0~1までの間に正規化する
        //正規化の式
        //格納されるデータをX、値域をxとした場合
        //X=X-xmin/xmax-xmin
        //今回の場合最低値はいずれも０なので減算を行う必要はなし
        //よってチャージタンク/チャージのmax値で正規化した値をとることができる
        image.fillAmount = moveScript.GetCharge_Tank() / moveScript.GetMaxCharge();
    }
}
