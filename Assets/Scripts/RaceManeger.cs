using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

 public　class RaceManeger : MonoBehaviour //レースの流れを管理するクラス　テキストへの処理も行う
 //2023年5月28日に修正を行いました
{
    static float goalTime;
    float goalTime_Second;
    int goalTime_minute;
    bool isStert;
    bool isGoal;
    [Header("スタートカウント何秒するか")]
    [SerializeField]
    float countDoun;
    float start_Time;
    int count_Num;
    [Header("ゴールまでに何周するか")]
    [SerializeField]
    int goal_Lap;
    int now_Lap;
    [SerializeField]
    List<AirRideMove>player=new List<AirRideMove>();
    [Header("ゴール関連")]
    [SerializeField]
    GameObject goal_Line;
    [SerializeField]
    GameObject goal_guard;
    [Header("テキスト")]
    [SerializeField]
    TextMeshProUGUI countDoun_Text;
    [SerializeField]
    TextMeshProUGUI time_Text;
    [SerializeField]
    TextMeshProUGUI lap_Text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCount());
        goal_Line.SetActive(false);
        goalTime = 0;
        goalTime_Second = 0;
        goalTime_minute = 0;
        now_Lap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (now_Lap == goal_Lap)//目標ラップに到達したら
        {
            StartCoroutine(Goal());
        }
        lap_Text.text = now_Lap.ToString() + "/" + goal_Lap.ToString();
        if (now_Lap - goal_Lap == 1)
        {
            lap_Text.color = Color.red;
        }
        Timer();
    }
    IEnumerator Goal()//ゴールした際の処理の関数
    {
        //ゴールした旨のテキスト表示
        isGoal = true;
        countDoun_Text.text = "GOAL";
        countDoun_Text.gameObject.SetActive(true);
        Debug.Log("GOAL");
        yield return new WaitForSeconds(5);
        SceneLoder.LoadResult();
    }
    IEnumerator StartCount()//カウントダウン
    {
        while (!isStert)
        {
            countDoun -= Time.deltaTime;//カウント減少+文字列に反映
            count_Num = (int)countDoun + 1;//
            countDoun_Text.text = count_Num.ToString();
            if (countDoun < 0)//カウントが0になったら開始
            {
                countDoun_Text.text = "Start";
                start_Time = Time.time;
                isStert = true;
                for (int i = 0; i < player.Count; i++)
                {
                    player[i].Race_Start();
                }
                yield return new WaitForSeconds(1);
                //しばらく待ってからテキストを消す
                countDoun_Text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    void Timer()
    {
        if (isStert&&!isGoal)//タイマーがスタートしていてかつゴールしてないとき
        {
            //時間を分：秒にフォーマットして表示
            goalTime = Time.time - start_Time;
            goalTime_Second =goalTime-goalTime_minute*60;
            time_Text.text = goalTime_minute+":"+goalTime_Second.ToString("00");
        }
        if (goalTime_Second > 60)
        {
            goalTime_minute++;
        }
    }
    public void Lap()
    {
        now_Lap++;
        goal_guard.SetActive(true);
        Debug.Log("LAP");
    }
    public void SetCanGall()//ほかのクラスからのコールバックを受け取ってゴール可能にする
    {
        goal_Line.SetActive(true);
        goal_guard.SetActive(false);
    }
    public static float GetTime()
    {
        return goalTime;
    }
}
