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
    GameObject active_Follow_Point;
    GameObject active_Point;
    int Point_Num;
    [Header("目標地点からどのくらい角度があるとブレーキするか")]
    [SerializeField]
    float brakeBorderLine;
    [Header("どんなコーナリングをするか")]
    [SerializeField]
    NPCCharacter cournaringCharacter;
    float followPointAngle;
    float angle;
    Vector3 cross;
    RaycastHit hit;
    void Start()
    {  
        Point_Num = 0;
        moveScript = this.GetComponent<AirRideMove>();
        senkai_Script = this.GetComponent<GetGround>();

       
        active_Follow_Point = follow_Points[Point_Num];
        active_Point = check_Points[Point_Num];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position-active_Follow_Point.transform.position;
        cross = Vector3.Cross(this.transform.forward,pos);
        angle = Vector3.Angle(this.transform.forward,pos)*cross.y;
        followPointAngle = angle * Mathf.Deg2Rad;
        Debug.DrawLine(this.transform.position,active_Follow_Point.transform.position, Color.red);
        NPCSenkai();
        PointManage();
    }
    void PointManage()
    {
        if (Physics.SphereCast(this.transform.position, this.transform.localScale.y / 2, this.transform.forward, out hit))
        {
            if (hit.collider.tag != "CheckPoint") { return; }    
            if (Vector3.SqrMagnitude(this.transform.position - hit.point) < 1f&&hit.collider.gameObject==active_Point)
            {
                if (Point_Num == check_Points.Length-1)
                {
                    Point_Num = 0;
                    active_Follow_Point=follow_Points[Point_Num];
                    active_Point = check_Points[Point_Num];
                    return;
                }
                Point_Num++;
				active_Follow_Point = follow_Points[Point_Num];
				active_Point = check_Points[Point_Num];
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
        //設定した値以上だった場合ブレーキ(push)をかける
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
