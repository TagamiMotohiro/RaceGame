using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class AirRideMove : MonoBehaviour�@//5��28���ɏC�����s���܂����B
{
    bool isStert;
    Vector3 velocity;//�ړ�����x�N�g��
    float speed;//�P�ʈړ����x
    [Header("�v���C���[�X�e�[�^�X")]
    [SerializeField]
    float max_Speed;//�ō���
    [SerializeField]
    float player_Height;//�v���C���[�̑傫���i���a�j
    [SerializeField]
    float push_friction;//A�{�^�����������Ƃ��̌�����
    [SerializeField]
    float accel;//����
    [SerializeField]
    float charge;//A�{�^�����������Ƃ��Ƀ^���N�ɂ��܂�͂̒P�ʗ�
    [SerializeField]
    float charge_Tank;//���݂̃`���[�W��
    [SerializeField]
    float Max_charge;//�`���[�W�̍ő�l
    [Header("�ǒT��RayCast�p��LayerMask")]
    [SerializeField]
    LayerMask collisionLayer;
    bool isPush;//Space��������Ă���
    bool pushRelesed;//Space�������ꂽ
    
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
            if (gameObject.tag!="Player") { return; }
            PushManage();
            VelocityUpdate();
            if (FowerdIsWall()) {
                CollisionWall_SpeedDown();
            }
            MacineMove();
        }
        
    }
	private void LateUpdate()
	{   
        
	}
    private void AccelManage()//���x���Ǘ�����֐�
    {
        if (speed <= max_Speed && !isPush)//�������Ă��炸�AA�{�^����������Ă��Ȃ��Ƃ�
        {
            if (charge_Tank >= Max_charge && pushRelesed)//A�{�^���������ꂽ�u�Ԃ��A�ő�܂Ń`���[�W����Ă�����
            {
                speed = max_Speed;//�����ɍō�����
            }
            charge_Tank = 0;//�����Ă���Ԃ̓`���[�W�O
            speed += accel * Time.deltaTime;//����
        }
        if (isPush==true)//A�{�^����������Ă�����
        {
            if (speed > 0)//���x���O�ȏ�̏ꍇ
            {
                speed -= push_friction * Time.deltaTime;//����
            }
            else
            {
                speed = 0;
            }
        }
        
    }
    
    private void CollisionWall_SpeedDown()//�ǂɓ����������̃R�[���o�b�N
    {
        this.speed -= speed / 2;
        if (this.speed < 0)
        {
          this.speed = 0;
        }
    }
    private void PushManage()//�v�b�V������Ă����Ԃ��Ǘ�����֐�
    {
        PushCtrl();
        if (isPush)
        {
            ChargeTankPlass();
        }
    }
    private void PushCtrl()//�{�^����������Ă��邩�ǂ������Ǘ�����֐�
    {
        if (this.gameObject.tag != "Player") { return; }//�v���C���[�ȊO�̏ꍇ�͑��얳��
        if (Input.GetKey(KeyCode.Space))//A�{�^�� or Space��������Ă�����
        {
            isPush = true;
        }
        else
        {
            isPush = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            pushRelesed = true;
        }
        else
        {
            pushRelesed = false;
        }
    }
    private void ChargeTankPlass()//�`���[�W����Ă���Ƃ��Ƀ^���N�Ƀp���[�����Z
    {
        if (charge_Tank <= Max_charge)
        {
            charge_Tank += charge * Time.deltaTime;//�`���[�W�����Z
        }
    }
    private void VelocityUpdate()
	{
	    velocity = (this.transform.forward*speed); //���ׂĂ�Update�I����ɍŏI�I�ɎZ�o���ꂽ���l���ړ��x�N�g���ɑ��
    }
    private void MacineMove() {
       this.transform.position += velocity*Time.deltaTime;//Velocity�����ƂɈړ�
    }
    
    public bool FowerdIsWall()//�ǂɓ������Ă��邩�̔���
    {
        if (Physics.SphereCast(this.transform.position, player_Height, this.transform.forward, out hit)/*���ʕ����̔���*/)
        {
            if (hit.collider.tag != "Wall" && (hit.collider.tag != "Player" && hit.collider.tag != "NPC")) { return false; }
            if (Vector3.SqrMagnitude(hit.point - this.transform.position) < 1f/*ray�����������|�C���g�Ǝ��g��position�Ԃ̃x�N�g���̑傫����1�����ɂȂ�����*/)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    //�ȉ�GetterPutter�֐�
    public void Race_Start()
    { 
        isStert=true;
    }
    public float GetPlayer_Height()
    {
        return player_Height;
    }
    public bool GetIsPush()
    {
        return isPush;
    }
    public void SetIsPush(bool b)
    {
        isPush = b;
    }
    public void SetIsPushRereced(bool b)
    {
        pushRelesed = b;
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
