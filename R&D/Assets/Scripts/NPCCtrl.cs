using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    public enum NPCCharacter
    {
        InCourse,
        Nomal,
        OutCourse
    }
    // Start is called before the first frame update
    //チェックポイント一覧
    [SerializeField]
    GameObject[] check_Points=new GameObject[0];
    [SerializeField]
    //チェックポイント内の実際に追うポイント一覧
    GameObject[] follow_Points=new GameObject[0];
	AirRideMove moveScript;
    GetGround senkai_Script;

    //チェックポイント管理関連
    GameObject active_FollowPoint;
    GameObject active_CheckPoint;
    int Point_Num;

    [Header("目標地点からどのくらい角度があるとブレーキするか")]
    [SerializeField]
    float brakeBorderLine;
    [Header("どんなコーナリングをするか")]
    [SerializeField]
    NPCCharacter cournaringCharacter;
    [Header("チェックポイント探索LayCastのレイヤーマスク")]
    [SerializeField]
    LayerMask checkPointMask;
    float followPointAngle;
    float angle;
    Vector3 cross;
    RaycastHit hit;
    
    void Start()
    {  
        Point_Num = 0;
        senkai_Script = this.GetComponent<GetGround>();
        moveScript = this.GetComponent<AirRideMove>();
        active_FollowPoint = follow_Points[Point_Num];
        active_CheckPoint = check_Points[Point_Num];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position-active_FollowPoint.transform.position;
        cross = Vector3.Cross(transform.forward,pos);
        angle = Vector3.Angle(transform.forward,pos)*cross.y;
        Debug.DrawLine(transform.position,active_FollowPoint.transform.position);
        followPointAngle = angle * Mathf.Deg2Rad;
        NPCSenkai();
        PointManage();
    }
    void PointManage()//チェックポイントの管理
    {
        if (Physics.SphereCast(transform.position,transform.localScale.y/2,transform.forward,out hit, checkPointMask))
        { 
            if (Vector3.SqrMagnitude(this.transform.position - hit.point) < 1f&&hit.collider.gameObject==active_CheckPoint)//チェックポイントに当たったら
            {
                if (Point_Num == check_Points.Length-1)
                {
                    Point_Num = 0;
                    active_FollowPoint=follow_Points[Point_Num];
                    active_CheckPoint = check_Points[Point_Num];
                    return;
                }
                Point_Num++;//ポイント番号を一つ進める
				active_FollowPoint = follow_Points[Point_Num];//一つ進めた番号のポイントを次に追跡開始
				active_CheckPoint = check_Points[Point_Num];
            }
        }
    }
    void NPCSenkai()//コースに合わせてNPCが旋回
    {
        //曲がるべきコーナーが緩やかだったらブレーキをかけずに旋回
        if (followPointAngle > 0)
        {
            senkai_Script.PlusHorizontal(-(senkai_Script.getSenkai() * Time.deltaTime));
        }
        if (followPointAngle < 0)
        {
            senkai_Script.PlusHorizontal(senkai_Script.getSenkai() * Time.deltaTime);
        }
        //設定した値以上急カーブだった場合ブレーキ(push)をかける
        if (followPointAngle > brakeBorderLine || followPointAngle < -brakeBorderLine)
        {
            moveScript.SetIsPush(true);
            return;
        }
        else
        {
            if (moveScript.GetIsPush())
            {
                moveScript.SetIsPushRereced(true);
            }
            else
            {
                moveScript.SetIsPushRereced(false);
            }
            moveScript.SetIsPush(false);
        }
    }
    //ゲッターセッター関数
    public NPCCharacter GetCharacter()
    {
        return cournaringCharacter;
    }
    public void AddCheckPoint(GameObject[] point)
    {
        check_Points = point;
    }
    public void AddFollowPoint(GameObject[] point)
    {
        follow_Points = point;
    }
}
