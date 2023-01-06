using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RaceManeger : MonoBehaviour
{
    public static float Goal_Time;
    public float Goal_Time_Second;
    public int Goal_minute;
    bool isStert;
    bool isGoal;
    bool timer_Stert;
    public float count_Doun;
    public float stert_Time;
    public int count_Num;
    public int goal_Lap;
    public int now_Lap;
    [SerializeField]
    private List<GameObject>player=new List<GameObject>();
    public GameObject goal_Line;
    public GameObject goal_guard;
    public GameObject goal_Text;
    public GameObject scene_Maneger;
    public TextMeshProUGUI countDoun_Text;
    public TextMeshProUGUI time_Text;
    // Start is called before the first frame update
    void Start()
    {
        goal_Line = GameObject.Find("Goal_Line");
        goal_Line.SetActive(false);
        player.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Goal_Time_Second = 0;
        Goal_minute = 0;
        now_Lap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (now_Lap == goal_Lap)
        {
            isGoal = true;
            goal_Text.SetActive(true);
            Debug.Log("GOAL");
            Invoke("Message_Goal",5f);
        }
        Stert_Count();
        Timer();
    }
    void Stert_Count()
    {
        if (isStert == false)
        {
            count_Doun -= Time.deltaTime;
            count_Num = (int)count_Doun+1;
            countDoun_Text.text = count_Num.ToString();
            if (count_Doun < 0)
            {
                countDoun_Text.text = "Start";
                isStert = true;
                Destroy(countDoun_Text);
                for (int i = 0; i < player.Count; i++) {
                    player[i].SendMessage("Race_Start");
                }
                stert_Time = Time.time;
            }
        }
    }
    void Timer()
    {
        if (isStert&&!isGoal)
        {
            Goal_Time = Time.time - stert_Time;
            Goal_Time_Second =Goal_Time-Goal_minute*60;
            time_Text.text = Goal_minute+":"+Goal_Time_Second.ToString("00");
            //time_Text.text = new TimeSpan(0,0,(int)Goal_Time).ToString();
        }
        if (Goal_Time_Second > 60)
        {
            Goal_minute++;
        }
    }
    void Lap()
    {
        now_Lap++;
        goal_guard.SetActive(true);
        Debug.Log("LAP");
    }
    void Reload()
    { 
        goal_Line.SetActive(true);
        goal_guard.SetActive(false);
    }
    void Message_Goal()
    {
        scene_Maneger.SendMessage("Load_Ranking");
	}
	public static TimeSpan Get_Goal_Time()
	{
        TimeSpan Goal_TimeSpan = new TimeSpan(0,0,0,0,(int)Goal_Time);  
        return Goal_TimeSpan;
    }
}
