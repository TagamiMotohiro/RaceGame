using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GetGround : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField]
    private AirRideMove moveScript;//プッシュされてるか確認用
    public float player_height;//プレイヤーの身長（浮遊してる分含め）
    public float fall_Speed;//落下速度
    float origin_FallSpeed;//初期の落下速度
    bool isground = true;//接地判定 
   
    public float senkai;//旋回性能
    public float horizontal;//x軸入力取得
    private float origin_Senkai;
    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = this.GetComponent<AirRideMove>();
        origin_Senkai = senkai;
        origin_FallSpeed = fall_Speed;//最初の落下速度を代入
    }
    
    // Update is called once per frame
    void Update()
    {
        CollisionWall();
        Player_Senkai();
        Ray ray = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray,out hit, player_height,3))//自身の存在するレイヤーを無視しrayを投射し判定を取得
        {
            if (hit.collider.tag == "CheckPoint") 
            { return; }
            isground = true;
        }
        else
        {
            isground=false;
        }
        if (isground)
        {
            IsGround_Rotate();
        }
        else
        {
            Fall();
        }
    }
	private void LateUpdate()
	{
        filled_correction();
	}
	void Fall()
    {  
        this.transform.position -=new Vector3(0, fall_Speed*Time.deltaTime, 0);//落下速度分自身のy座標を減少
        if (moveScript.GetIsPush())
        {
            if (fall_Speed == origin_FallSpeed/*プッシュされていたら＆落下速度が変わっていなかったら*/)
            {
                fall_Speed = 50;//落下速度上昇
            }
        }
        else
        {
            fall_Speed = origin_FallSpeed;
        }
    }
    void IsGround_Rotate()
    {
		//接地している
		Quaternion q = Quaternion.FromToRotation(Vector3.up, hit.normal);
        //床の法線ベクトルとY軸単位ベクトルから接地するための回転を生成
		this.transform.rotation = q * this.transform.rotation;
        //現在の回転に対して掛け合わせる

		//pos.y = hit.point.y + player_height;
		//transform.position = pos;//transformに代入
		//this.transform.LookAt(this.transform.rotation*hit.transform.forward);
		//this.transform.rotation = Quaternion.Euler(rotate);
		//this.transform.rotation = Quaternion.Euler(hit.transform.eulerAngles.x, this.transform.eulerAngles.y,hit.transform.eulerAngles.z);//取得した回転に補正
		//this.transform.rotation = Quaternion.FromToRotation(transform.up,hit.normal);
		//this.transform.up = hit.normal;
		//Debug.Log("接地判定"+hit.collider.gameObject.name);
	}
    void filled_correction()//地形埋まり防止
    {
        //Debug.Log(this.transform.position.y-hit.point.y);
        if (this.transform.position.y - hit.point.y < player_height - 0.1f)//プレイヤーが（プレイヤーの身長-誤差許容分）分浮いていなければ
        {
            //Debug.Log("埋まっております");
            this.transform.position = new Vector3(this.transform.position.x, player_height + hit.point.y, this.transform.position.z);
            //埋まり補正
        }
        else
        {
            //Debug.Log("埋まっておりません");
        }
    }
    private void CollisionWall()
    {
        if (this.moveScript.FowerdIsWall())
        {
            this.transform.rotation = Quaternion.LookRotation(moveScript.GetWall_Nomal()+this.transform.forward, this.transform.up);//反射先のベクトルに自身を回転
            horizontal = (Mathf.Atan2(this.transform.forward.x,this.transform.forward.z)*Mathf.Rad2Deg);//今自身が向いている角度にhorizontalを補正
        }//壁に当たった時の処理
        
    }
    void Player_Senkai()
    {
        //Debug.Log(horizontal.ToString()+moveScript.GetWall_Nomal());
        horizontal = Mathf.Repeat(horizontal, 360);
        this.transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up); //旋回
        if (this.gameObject.name != "Player") { return; }
        horizontal += (Input.GetAxis("Horizontal") * senkai) * Time.deltaTime;//旋回する値を取得
        if (moveScript.GetIsPush() && senkai == origin_Senkai)
        {
            senkai *= 5;
        }
        else
        {
            senkai = origin_Senkai;
        }
    }
    public float getSenkai()
    {
        return senkai;
    }
}
