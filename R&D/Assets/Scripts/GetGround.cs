using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GetGround : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField]
    private AirRideMove moveScript;//�v�b�V������Ă邩�m�F�p
    public float player_height;//�v���C���[�̐g���i���V���Ă镪�܂߁j
    public float fall_Speed;//�������x
    float origin_FallSpeed;//�����̗������x
    bool isground = true;//�ڒn���� 
   
    public float senkai;//���񐫔\
    public float horizontal;//x�����͎擾
    private float origin_Senkai;
    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = this.GetComponent<AirRideMove>();
        origin_Senkai = senkai;
        origin_FallSpeed = fall_Speed;//�ŏ��̗������x����
    }
    
    // Update is called once per frame
    void Update()
    {
        CollisionWall();
        Player_Senkai();
        Ray ray = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray,out hit, player_height,3))//���g�̑��݂��郌�C���[�𖳎���ray�𓊎˂�������擾
        {
            if (hit.collider.tag == "CheckPoint") 
            { return; }
            isground = true;
        }
        else
        {
            isground=false;
        }
        if (isground)
        {
            IsGround_Rotate();
        }
        else
        {
            Fall();
        }
    }
	private void LateUpdate()
	{
        filled_correction();
	}
	void Fall()
    {  
        this.transform.position -=new Vector3(0, fall_Speed*Time.deltaTime, 0);//�������x�����g��y���W������
        if (moveScript.GetIsPush())
        {
            if (fall_Speed == origin_FallSpeed/*�v�b�V������Ă����灕�������x���ς���Ă��Ȃ�������*/)
            {
                fall_Speed = 50;//�������x�㏸
            }
        }
        else
        {
            fall_Speed = origin_FallSpeed;
        }
    }
    void IsGround_Rotate()
    {
		//�ڒn���Ă���
		Quaternion q = Quaternion.FromToRotation(Vector3.up, hit.normal);
        //���̖@���x�N�g����Y���P�ʃx�N�g������ڒn���邽�߂̉�]�𐶐�
		this.transform.rotation = q * this.transform.rotation;
        //���݂̉�]�ɑ΂��Ċ|�����킹��

		//pos.y = hit.point.y + player_height;
		//transform.position = pos;//transform�ɑ��
		//this.transform.LookAt(this.transform.rotation*hit.transform.forward);
		//this.transform.rotation = Quaternion.Euler(rotate);
		//this.transform.rotation = Quaternion.Euler(hit.transform.eulerAngles.x, this.transform.eulerAngles.y,hit.transform.eulerAngles.z);//�擾������]�ɕ␳
		//this.transform.rotation = Quaternion.FromToRotation(transform.up,hit.normal);
		//this.transform.up = hit.normal;
		//Debug.Log("�ڒn����"+hit.collider.gameObject.name);
	}
    void filled_correction()//�n�`���܂�h�~
    {
        //Debug.Log(this.transform.position.y-hit.point.y);
        if (this.transform.position.y - hit.point.y < player_height - 0.1f)//�v���C���[���i�v���C���[�̐g��-�덷���e���j�������Ă��Ȃ����
        {
            //Debug.Log("���܂��Ă���܂�");
            this.transform.position = new Vector3(this.transform.position.x, player_height + hit.point.y, this.transform.position.z);
            //���܂�␳
        }
        else
        {
            //Debug.Log("���܂��Ă���܂���");
        }
    }
    private void CollisionWall()
    {
        if (this.moveScript.FowerdIsWall())
        {
            this.transform.rotation = Quaternion.LookRotation(moveScript.GetWall_Nomal()+this.transform.forward, this.transform.up);//���ː�̃x�N�g���Ɏ��g����]
            horizontal = (Mathf.Atan2(this.transform.forward.x,this.transform.forward.z)*Mathf.Rad2Deg);//�����g�������Ă���p�x��horizontal��␳
        }//�ǂɓ����������̏���
        
    }
    void Player_Senkai()
    {
        //Debug.Log(horizontal.ToString()+moveScript.GetWall_Nomal());
        horizontal = Mathf.Repeat(horizontal, 360);
        this.transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up); //����
        if (this.gameObject.name != "Player") { return; }
        horizontal += (Input.GetAxis("Horizontal") * senkai) * Time.deltaTime;//���񂷂�l���擾
        if (moveScript.GetIsPush() && senkai == origin_Senkai)
        {
            senkai *= 5;
        }
        else
        {
            senkai = origin_Senkai;
        }
    }
    public float getSenkai()
    {
        return senkai;
    }
}
