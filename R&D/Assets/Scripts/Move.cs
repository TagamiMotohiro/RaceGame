using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    private float player_Radius;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        player_Radius = this.transform.localScale.z / 2;
    }

    // Update is called once per frame
    void Update(){
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
        //4方向分の壁判定取得ray宣言
        RaycastHit[] hit=new RaycastHit[4];
        vertical=1;
        if(Physics.SphereCast(this.transform.position,0.5f,Vector3.forward,out hit[0]))//正面方向の判定
        {//飛ばしたrayがhitしたか見る
            Debug.Log("正面レイ"+hit[0].point);
            if (hit[0].point.z<=this.transform.position.z+player_Radius/*rayが当たったポイントがプレイヤーの半径内に侵入したら*/)
            {
                //壁に接触した判定
                //壁に当たったらmathf.Clampでそれ以上進めないように値域を制限
				vertical = Mathf.Clamp(Input.GetAxis("Horizontal"),-1.0f,0);

                Debug.Log("壁判定（正面）" + this.transform.position);
				//遺物　位置を補正する形で壁判定
				//{ this.transform.position = new Vector3(transform.position.x, transform.position.y, hit.point.z - player_Radius); }

			}
        }//他３方向も繰り返す
        if (Physics.SphereCast(this.transform.position, 0.5f,-(Vector3.forward), out hit[1]))
        {
            Debug.Log("後ろレイ" + hit[1].point);
            if (hit[1].point.z >=this.transform.position.z-player_Radius)
            {
                vertical = Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1.0f);
                Debug.Log("壁判定（後ろ）" + this.transform.position);
            }
        }
        if (Physics.SphereCast(this.transform.position, 0.5f, Vector3.right, out hit[2]))
        {
            Debug.Log("右面レイ" + hit[2].point);
            if (hit[2].point.x <= this.transform.position.x+player_Radius)
            {
                horizontal = Mathf.Clamp(Input.GetAxis("Horizontal"), -1.0f, 0f);
                Debug.Log("壁判定（右面）" + this.transform.position);
            }
            
        }
        if (Physics.SphereCast(this.transform.position, 0.5f, -(Vector3.right), out hit[3]))
        {
           Debug.Log("左面レイ" + hit[3].point);
           if (hit[3].point.x >= this.transform.position.x-player_Radius)
           {
             horizontal = Mathf.Clamp(Input.GetAxis("Horizontal"), 0f, 1.0f);
             Debug.Log("壁判定（左面）" + this.transform.position);
           }
         }
        this.transform.Translate(horizontal*(moveSpeed*Time.deltaTime),0,vertical*(moveSpeed*Time.deltaTime));
    } 
}
    