using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    static int a = 10;
    const int i = 10;
    bool[,] aa= new bool[a,i];
    // Start is called before the first frame update
    List<GameObject> check_Points = new List<GameObject>();
	List<GameObject> follow_Points = new List<GameObject>();
	AirRideMove move_Script;
    GetGround senkai_Script;
    GameObject active_Follow_Point;
    GameObject active_Point;
    int Point_Num;
    [SerializeField]
    int character_Num;
    [SerializeField]
    float angle_Rad;
    [SerializeField]
    float direction;
    [SerializeField]
    float angle;
    [SerializeField]
    Vector3 cross;
    RaycastHit hit;
    void Start()
    {
        move_Script = this.GetComponent<AirRideMove>();
        senkai_Script = this.GetComponent<GetGround>();
        Point_Num = 0;
        for (int i = 0; i < 25; i++)
        {
            check_Points.Add(GameObject.Find("Point (" + i.ToString("D2") + ")"));
            //フィールド全域のPointを取得
        }
        switch (character_Num)//性格の値によってコース取りを変化させる
        { 
            case 0:
                for (int i = 0; i < check_Points.Count; i++)
                {
                    follow_Points.Add(GameObject.Find("Point (" + i.ToString("D2") + ")"));
                }
            break;
            case 1:
				for (int i = 0; i < check_Points.Count; i++)
				{
					follow_Points.Add(GameObject.Find("Point (" + i.ToString("D2") + ")").transform.GetChild(0).gameObject);
				}
            break ;
            case 2:
				for(int i = 0; i < check_Points.Count; i++)
				{
					follow_Points.Add(GameObject.Find("Point (" + i.ToString("D2") + ")").transform.GetChild(1).gameObject);
				}
             break;
		}
        active_Follow_Point = follow_Points[Point_Num];
        active_Point = check_Points[Point_Num];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position-active_Follow_Point.transform.position;
        cross = Vector3.Cross(this.transform.forward,pos);
        angle = Vector3.Angle(this.transform.forward,pos)*cross.y;
        angle_Rad = angle * Mathf.Deg2Rad;
        Debug.DrawLine(this.transform.position,active_Follow_Point.transform.position, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.position+cross, Color.green);
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
                if (Point_Num == check_Points.Count-1)
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
    void GetNPCCharacter(int num)
    {
        this.character_Num = num;
    }
    void NPCSenkai()
    {
        if (angle_Rad > 0)
        {
            senkai_Script.horizontal-=senkai_Script.getSenkai()*Time.deltaTime;
        }
        if (angle_Rad < 0)
        {
            senkai_Script.horizontal+=senkai_Script.getSenkai()*Time.deltaTime;
        }
        if (angle_Rad > 30 || angle_Rad < -30)
        {
            move_Script.isPush = true;
            return;
        }
        else
        {
            if (move_Script.GetIsPush())
            {
                move_Script.pushRelesed = true;
            }
            else {
                move_Script.pushRelesed = false;
            }
            move_Script.isPush = false;
        }
    }
}
