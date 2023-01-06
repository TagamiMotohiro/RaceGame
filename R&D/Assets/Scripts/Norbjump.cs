using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Norbjump : MonoBehaviour
{//�W�����v�����R�������ڒn����Ǘ��֐�
 //���X�ʁX�̃X�N���v�g���������󂠂��ē�������
    private float speed;//�W�����v�ŒP�ʎ��ԕ��ړ����鋗��
    public float power;//�W�����v��
    public float g;//�d�͒萔
    public float t;//���Ԓ萔
    public float v;//�����ňړ����鋗��
    bool isJumped;//�W�����v�����u�Ԃ̂�ON
    bool jumped;//�W�����v���Ă����ON
    bool isGround;//�n�ʂɗ����Ă�����ON
    // Start is called before the first frame update
    void Start()
    {
        //������
        jumped = false;
        isJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, Vector3.down);
        Debug.DrawRay(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, this.transform.localScale.x / 2)/*���g�̒��S�_���玩�g�̔��a���̒�����ray������������*/)
        {//�ڒn���聕���ݒn��n��ɕ␳
            isGround = true;
            this.transform.position = hit.point + new Vector3(0, this.transform.localScale.y / 2, 0);
            Debug.Log("�ڒn");
        }
        else {
            isGround = false;
        }
        jump();
    }
    void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&isGround/*���n���Ă���Ԃ�Space�L�[*/)
        {
            isJumped=true;//�W�����v�����u�ԂȂ̂�ON
            Debug.Log("JUMP="+isJumped);
        }
        if(isJumped)
        {
            speed+=power;//�W�����v�ňړ�����X�s�[�h�ɏ����W�����v�͂���
            isJumped=false;//�W�����v�����u�Ԃł͂Ȃ��Ȃ����̂�OFF
            jumped=true;//�W�����v���ɂȂ����̂�ON
        }

        if (jumped/*�W�����v���ɍs������*/)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (speed * Time.deltaTime), this.transform.position.z);//Speed�����x���㏸
            speed = speed - (g * Time.deltaTime);//�d�͒萔���X�s�[�h������
            if (speed <= 3/*������x���x�������Ă�����*/)
            {
                speed = 0;//���S�Ɍ���������
                Debug.Log("JUMP=" + isJumped);
                jumped = false;//�W�����v�I��
            }
        }
        else
        {
            if (!isGround/*�W�����v�I�����n�ʂɂ��Ă��Ȃ����*/)
            {
                Debug.Log("���R�����J�n");
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-v, this.transform.position.z);//���ݒn��P�ʎ��Ԃ̗����X�s�[�h�����Z
                v = t/g;//���ԁ��d�͒萔�ő��x���v�Z
                t += Time.deltaTime;//���ԁ��ŏI�t���[������̌o�ߎ���
            }
            else {//�n�ʂɂ�����
                Debug.Log("���R��������");
                v = 0;//���x���O�ɖ߂�
                t = 0;
            }
        }
        
    }
    public bool returnisJumped()
    {   
        return isJumped;    
    }
    public bool returnJumped()
    {
        return jumped;
    }
}
