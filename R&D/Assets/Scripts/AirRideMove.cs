using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class AirRideMove : MonoBehaviour
{
    //ボタン名はnitendo switch pro コントローラーを想定して記載
    [SerializeField]
    private float max_Speed;//最高速
    private float speed;//単位移動速度
    public bool isStert;
    private Vector3 velocity;//移動するベクトル
    private float player_Height;//プレイヤーの大きさ（半径）
    public float push_friction;//Aボタンを押したときの減速率
    public float kasoku;//加速
    public float charge;//Aボタンを押したときにタンクにたまる力の単位量
    public float charge_Tank;//現在のチャージ量
    public float Max_charge;//チャージの最大値
    public bool isPush;//Aボタンが押されている
    public bool pushRelesed;//Aボタンが離された
    
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
            
        }
        PushManage();
    }
	private void LateUpdate()
	{
        VelocityUpdate();
        Latemove();
        CollisionWall_SpeedDown();
	}
    private void AccelManage()
    {
        if (speed <= max_Speed && !isPush)//加速しておらず、Aボタンが押されていないとき
        {
            if (charge_Tank >= Max_charge && pushRelesed)//Aボタンが離された瞬間かつ、最大までチャージされていた時
            {
                speed = max_Speed;//即座に最高速に
            }
            charge_Tank = 0;//走っている間はチャージ０
            speed += kasoku * Time.deltaTime;//加速
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
    public bool FowerdIsWall()//壁に当たっているかの判定
    {
        if (Physics.SphereCast(this.transform.position, player_Height, this.transform.forward, out hit)/*正面方向の判定*/)
        {
            if (hit.collider.tag != "Wall"&&hit.collider.tag!="Player"){ return false;}
            if (Vector3.SqrMagnitude(hit.point - this.transform.position) < 1f/*rayが当たったポイントと自身のposition間のベクトルの大きさが1未満になったら*/)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    private void CollisionWall_SpeedDown()
    {
        if (!FowerdIsWall()) { return; }
        Debug.Log(this.gameObject.name + "壁に衝突し、減速した");
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
            ChargePlass();
        }
    }
    private void PushCtrl()
    {
        if (this.gameObject.name != "Player") { return; }
        if (/*Gamepad.current.buttonEast.isPressed||*/Input.GetKey(KeyCode.Space))//Aボタン or Spaceが押されていたら
        {
            isPush = true;
        }
        else
        {
            isPush = false;
        }

        if (/*Gamepad.current.buttonEast.wasReleasedThisFrame||*/Input.GetKeyUp(KeyCode.Space))
        {
            pushRelesed = true;
        }
        else
        {
            pushRelesed = false;
        }
    }
    private void ChargePlass()
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
    private void Latemove() {
       this.transform.position += velocity*Time.deltaTime;//Velocityをもとに移動
    }
    private void Race_Start()
    { 
        isStert=true;
    }
    //以下数値渡し関数
    public float GetPlayer_Height()
    {
        return player_Height;
    }
    public bool GetIsPush()
    {
        return isPush;
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
