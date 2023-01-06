using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class AirRideMove : MonoBehaviour
{
    //�{�^������nitendo switch pro �R���g���[���[��z�肵�ċL��
    [SerializeField]
    private float max_Speed;//�ō���
    private float speed;//�P�ʈړ����x
    public bool isStert;
    private Vector3 velocity;//�ړ�����x�N�g��
    private float player_Height;//�v���C���[�̑傫���i���a�j
    public float push_friction;//A�{�^�����������Ƃ��̌�����
    public float kasoku;//����
    public float charge;//A�{�^�����������Ƃ��Ƀ^���N�ɂ��܂�͂̒P�ʗ�
    public float charge_Tank;//���݂̃`���[�W��
    public float Max_charge;//�`���[�W�̍ő�l
    public bool isPush;//A�{�^����������Ă���
    public bool pushRelesed;//A�{�^���������ꂽ
    
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
            
        }
        PushManage();
    }
	private void LateUpdate()
	{
        VelocityUpdate();
        Latemove();
        CollisionWall_SpeedDown();
	}
    private void AccelManage()
    {
        if (speed <= max_Speed && !isPush)//�������Ă��炸�AA�{�^����������Ă��Ȃ��Ƃ�
        {
            if (charge_Tank >= Max_charge && pushRelesed)//A�{�^���������ꂽ�u�Ԃ��A�ő�܂Ń`���[�W����Ă�����
            {
                speed = max_Speed;//�����ɍō�����
            }
            charge_Tank = 0;//�����Ă���Ԃ̓`���[�W�O
            speed += kasoku * Time.deltaTime;//����
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
    public bool FowerdIsWall()//�ǂɓ������Ă��邩�̔���
    {
        if (Physics.SphereCast(this.transform.position, player_Height, this.transform.forward, out hit)/*���ʕ����̔���*/)
        {
            if (hit.collider.tag != "Wall"&&hit.collider.tag!="Player"){ return false;}
            if (Vector3.SqrMagnitude(hit.point - this.transform.position) < 1f/*ray�����������|�C���g�Ǝ��g��position�Ԃ̃x�N�g���̑傫����1�����ɂȂ�����*/)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    private void CollisionWall_SpeedDown()
    {
        if (!FowerdIsWall()) { return; }
        Debug.Log(this.gameObject.name + "�ǂɏՓ˂��A��������");
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
            ChargePlass();
        }
    }
    private void PushCtrl()
    {
        if (this.gameObject.name != "Player") { return; }
        if (/*Gamepad.current.buttonEast.isPressed||*/Input.GetKey(KeyCode.Space))//A�{�^�� or Space��������Ă�����
        {
            isPush = true;
        }
        else
        {
            isPush = false;
        }

        if (/*Gamepad.current.buttonEast.wasReleasedThisFrame||*/Input.GetKeyUp(KeyCode.Space))
        {
            pushRelesed = true;
        }
        else
        {
            pushRelesed = false;
        }
    }
    private void ChargePlass()
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
    private void Latemove() {
       this.transform.position += velocity*Time.deltaTime;//Velocity�����ƂɈړ�
    }
    private void Race_Start()
    { 
        isStert=true;
    }
    //�ȉ����l�n���֐�
    public float GetPlayer_Height()
    {
        return player_Height;
    }
    public bool GetIsPush()
    {
        return isPush;
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
