using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class AirRideMove : MonoBehaviour　//5月28日に修正を行いました。
{
    bool isStert;
    Vector3 velocity;//移動するベクトル
    float speed;//単位移動速度
    [Header("プレイヤーステータス")]
    [SerializeField]
    float max_Speed;//最高速
    [SerializeField]
    float player_Height;//プレイヤーの大きさ（半径）
    [SerializeField]
    float push_friction;//Aボタンを押したときの減速率
    [SerializeField]
    float accel;//加速
    [SerializeField]
    float charge;//Aボタンを押したときにタンクにたまる力の単位量
    [SerializeField]
    float charge_Tank;//現在のチャージ量
    [SerializeField]
    float Max_charge;//チャージの最大値
    [Header("壁探索RayCast用のLayerMask")]
    [SerializeField]
    LayerMask collisionLayer;
    bool isPush;//Spaceが押されている
    bool pushRelesed;//Spaceが離された
    
    RaycastHit hit = new RaycastHit();
    
    // Start is called before the first frame update
    void Start()
    {
        player_Height = this.transform.localScale.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStert)
        {
            AccelManage();
            if (gameObject.tag!="Player") { return; }
            PushManage();
            VelocityUpdate();
            if (FowerdIsWall()) {
                CollisionWall_SpeedDown();
            }
            MacineMove();
        }
        
    }
	private void LateUpdate()
	{   
        
	}
    private void AccelManage()//速度を管理する関数
    {
        if (speed <= max_Speed && !isPush)//加速しておらず、Aボタンが押されていないとき
        {
            if (charge_Tank >= Max_charge && pushRelesed)//Aボタンが離された瞬間かつ、最大までチャージされていた時
            {
                speed = max_Speed;//即座に最高速に
            }
            charge_Tank = 0;//走っている間はチャージ０
            speed += accel * Time.deltaTime;//加速
        }
        if (isPush==true)//Aボタンが押されていたら
        {
            if (speed > 0)//速度が０以上の場合
            {
                speed -= push_friction * Time.deltaTime;//減速
            }
            else
            {
                speed = 0;
            }
        }
        
    }
    
    private void CollisionWall_SpeedDown()//壁に当たった時のコールバック
    {
        this.speed -= speed / 2;
        if (this.speed < 0)
        {
          this.speed = 0;
        }
    }
    private void PushManage()//プッシュされている状態か管理する関数
    {
        PushCtrl();
        if (isPush)
        {
            ChargeTankPlass();
        }
    }
    private void PushCtrl()//ボタンが押されているかどうかを管理する関数
    {
        if (this.gameObject.tag != "Player") { return; }//プレイヤー以外の場合は操作無効
        if (Input.GetKey(KeyCode.Space))//Aボタン or Spaceが押されていたら
        {
            isPush = true;
        }
        else
        {
            isPush = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            pushRelesed = true;
        }
        else
        {
            pushRelesed = false;
        }
    }
    private void ChargeTankPlass()//チャージされているときにタンクにパワーを加算
    {
        if (charge_Tank <= Max_charge)
        {
            charge_Tank += charge * Time.deltaTime;//チャージを加算
        }
    }
    private void VelocityUpdate()
	{
	    velocity = (this.transform.forward*speed); //すべてのUpdate終了後に最終的に算出された数値を移動ベクトルに代入
    }
    private void MacineMove() {
       this.transform.position += velocity*Time.deltaTime;//Velocityをもとに移動
    }
    
    public bool FowerdIsWall()//壁に当たっているかの判定
    {
        if (Physics.SphereCast(this.transform.position, player_Height, this.transform.forward, out hit)/*正面方向の判定*/)
        {
            if (hit.collider.tag != "Wall" && (hit.collider.tag != "Player" && hit.collider.tag != "NPC")) { return false; }
            if (Vector3.SqrMagnitude(hit.point - this.transform.position) < 1f/*rayが当たったポイントと自身のposition間のベクトルの大きさが1未満になったら*/)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    //以下GetterPutter関数
    public void Race_Start()
    { 
        isStert=true;
    }
    public float GetPlayer_Height()
    {
        return player_Height;
    }
    public bool GetIsPush()
    {
        return isPush;
    }
    public void SetIsPush(bool b)
    {
        isPush = b;
    }
    public void SetIsPushRereced(bool b)
    {
        pushRelesed = b;
    }
    public float GetCharge()
    {
        return charge;
    }
    public float GetCharge_Tank()
    {
        return charge_Tank;
    }
    public float GetMaxCharge()
    {
        return Max_charge;
    }
    public float GetVelocity_Magnitude()
    {
        return velocity.magnitude;
    }
    public Vector3 GetWall_Nomal()
    {
        return hit.normal;
    }
}   
