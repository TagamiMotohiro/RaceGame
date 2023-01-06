using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Norbjump : MonoBehaviour
{//ジャンプ＆自由落下＆接地判定管理関数
 //元々別々のスクリプトだったが訳あって統合した
    private float speed;//ジャンプで単位時間分移動する距離
    public float power;//ジャンプ力
    public float g;//重力定数
    public float t;//時間定数
    public float v;//落下で移動する距離
    bool isJumped;//ジャンプした瞬間のみON
    bool jumped;//ジャンプしている間ON
    bool isGround;//地面に立っていたらON
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        jumped = false;
        isJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, Vector3.down);
        Debug.DrawRay(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, this.transform.localScale.x / 2)/*自身の中心点から自身の半径分の長さのrayを下向き発射*/)
        {//接地判定＆現在地を地上に補正
            isGround = true;
            this.transform.position = hit.point + new Vector3(0, this.transform.localScale.y / 2, 0);
            Debug.Log("接地");
        }
        else {
            isGround = false;
        }
        jump();
    }
    void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&isGround/*着地している間にSpaceキー*/)
        {
            isJumped=true;//ジャンプした瞬間なのでON
            Debug.Log("JUMP="+isJumped);
        }
        if(isJumped)
        {
            speed+=power;//ジャンプで移動するスピードに初期ジャンプ力を代入
            isJumped=false;//ジャンプした瞬間ではなくなったのでOFF
            jumped=true;//ジャンプ中になったのでON
        }

        if (jumped/*ジャンプ中に行う処理*/)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (speed * Time.deltaTime), this.transform.position.z);//Speed分高度が上昇
            speed = speed - (g * Time.deltaTime);//重力定数分スピードが減少
            if (speed <= 3/*ある程度速度が衰えてきたら*/)
            {
                speed = 0;//完全に減衰させる
                Debug.Log("JUMP=" + isJumped);
                jumped = false;//ジャンプ終了
            }
        }
        else
        {
            if (!isGround/*ジャンプ終了時地面についていなければ*/)
            {
                Debug.Log("自由落下開始");
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-v, this.transform.position.z);//現在地を単位時間の落下スピード分減算
                v = t/g;//時間÷重力定数で速度を計算
                t += Time.deltaTime;//時間＝最終フレームからの経過時間
            }
            else {//地面についたら
                Debug.Log("自由落下完了");
                v = 0;//速度を０に戻す
                t = 0;
            }
        }
        
    }
    public bool returnisJumped()
    {   
        return isJumped;    
    }
    public bool returnJumped()
    {
        return jumped;
    }
}
