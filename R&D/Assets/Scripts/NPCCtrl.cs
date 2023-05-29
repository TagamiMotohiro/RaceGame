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
    //�`�F�b�N�|�C���g�ꗗ
    [SerializeField]
    GameObject[] check_Points=new GameObject[0];
    [SerializeField]
    //�`�F�b�N�|�C���g���̎��ۂɒǂ��|�C���g�ꗗ
    GameObject[] follow_Points=new GameObject[0];
	AirRideMove moveScript;
    GetGround senkai_Script;

    //�`�F�b�N�|�C���g�Ǘ��֘A
    GameObject active_FollowPoint;
    GameObject active_CheckPoint;
    int Point_Num;

    [Header("�ڕW�n�_����ǂ̂��炢�p�x������ƃu���[�L���邩")]
    [SerializeField]
    float brakeBorderLine;
    [Header("�ǂ�ȃR�[�i�����O�����邩")]
    [SerializeField]
    NPCCharacter cournaringCharacter;
    [Header("�`�F�b�N�|�C���g�T��LayCast�̃��C���[�}�X�N")]
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
    void PointManage()//�`�F�b�N�|�C���g�̊Ǘ�
    {
        if (Physics.SphereCast(transform.position,transform.localScale.y/2,transform.forward,out hit, checkPointMask))
        { 
            if (Vector3.SqrMagnitude(this.transform.position - hit.point) < 1f&&hit.collider.gameObject==active_CheckPoint)//�`�F�b�N�|�C���g�ɓ���������
            {
                if (Point_Num == check_Points.Length-1)
                {
                    Point_Num = 0;
                    active_FollowPoint=follow_Points[Point_Num];
                    active_CheckPoint = check_Points[Point_Num];
                    return;
                }
                Point_Num++;//�|�C���g�ԍ�����i�߂�
				active_FollowPoint = follow_Points[Point_Num];//��i�߂��ԍ��̃|�C���g�����ɒǐՊJ�n
				active_CheckPoint = check_Points[Point_Num];
            }
        }
    }
    void NPCSenkai()//�R�[�X�ɍ��킹��NPC������
    {
        //�Ȃ���ׂ��R�[�i�[���ɂ₩��������u���[�L���������ɐ���
        if (followPointAngle > 0)
        {
            senkai_Script.PlusHorizontal(-(senkai_Script.getSenkai() * Time.deltaTime));
        }
        if (followPointAngle < 0)
        {
            senkai_Script.PlusHorizontal(senkai_Script.getSenkai() * Time.deltaTime);
        }
        //�ݒ肵���l�ȏ�}�J�[�u�������ꍇ�u���[�L(push)��������
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
    //�Q�b�^�[�Z�b�^�[�֐�
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
