using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

 public�@class RaceManeger : MonoBehaviour //���[�X�̗�����Ǘ�����N���X�@�e�L�X�g�ւ̏������s��
 //2023�N5��28���ɏC�����s���܂���
{
    static float goalTime;
    float goalTime_Second;
    int goalTime_minute;
    bool isStert;
    bool isGoal;
    [Header("�X�^�[�g�J�E���g���b���邩")]
    [SerializeField]
    float countDoun;
    float start_Time;
    int count_Num;
    [Header("�S�[���܂łɉ������邩")]
    [SerializeField]
    int goal_Lap;
    int now_Lap;
    [SerializeField]
    List<AirRideMove>player=new List<AirRideMove>();
    [Header("�S�[���֘A")]
    [SerializeField]
    GameObject goal_Line;
    [SerializeField]
    GameObject goal_guard;
    [Header("�e�L�X�g")]
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
        if (now_Lap == goal_Lap)//�ڕW���b�v�ɓ��B������
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
    IEnumerator Goal()//�S�[�������ۂ̏����̊֐�
    {
        //�S�[�������|�̃e�L�X�g�\��
        isGoal = true;
        countDoun_Text.text = "GOAL";
        countDoun_Text.gameObject.SetActive(true);
        Debug.Log("GOAL");
        yield return new WaitForSeconds(5);
        SceneLoder.LoadResult();
    }
    IEnumerator StartCount()//�J�E���g�_�E��
    {
        while (!isStert)
        {
            countDoun -= Time.deltaTime;//�J�E���g����+������ɔ��f
            count_Num = (int)countDoun + 1;//
            countDoun_Text.text = count_Num.ToString();
            if (countDoun < 0)//�J�E���g��0�ɂȂ�����J�n
            {
                countDoun_Text.text = "Start";
                start_Time = Time.time;
                isStert = true;
                for (int i = 0; i < player.Count; i++)
                {
                    player[i].Race_Start();
                }
                yield return new WaitForSeconds(1);
                //���΂炭�҂��Ă���e�L�X�g������
                countDoun_Text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    void Timer()
    {
        if (isStert&&!isGoal)//�^�C�}�[���X�^�[�g���Ă��Ă��S�[�����ĂȂ��Ƃ�
        {
            //���Ԃ𕪁F�b�Ƀt�H�[�}�b�g���ĕ\��
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
    public void SetCanGall()//�ق��̃N���X����̃R�[���o�b�N���󂯎���ăS�[���\�ɂ���
    {
        goal_Line.SetActive(true);
        goal_guard.SetActive(false);
    }
    public static float GetTime()
    {
        return goalTime;
    }
}
