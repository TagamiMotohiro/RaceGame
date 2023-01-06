using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    private float player_Radius;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        player_Radius = this.transform.localScale.z / 2;
    }

    // Update is called once per frame
    void Update(){
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
        //4�������̕ǔ���擾ray�錾
        RaycastHit[] hit=new RaycastHit[4];
        vertical=1;
        if(Physics.SphereCast(this.transform.position,0.5f,Vector3.forward,out hit[0]))//���ʕ����̔���
        {//��΂���ray��hit����������
            Debug.Log("���ʃ��C"+hit[0].point);
            if (hit[0].point.z<=this.transform.position.z+player_Radius/*ray�����������|�C���g���v���C���[�̔��a���ɐN��������*/)
            {
                //�ǂɐڐG��������
                //�ǂɓ���������mathf.Clamp�ł���ȏ�i�߂Ȃ��悤�ɒl��𐧌�
				vertical = Mathf.Clamp(Input.GetAxis("Horizontal"),-1.0f,0);

                Debug.Log("�ǔ���i���ʁj" + this.transform.position);
				//�╨�@�ʒu��␳����`�ŕǔ���
				//{ this.transform.position = new Vector3(transform.position.x, transform.position.y, hit.point.z - player_Radius); }

			}
        }//���R�������J��Ԃ�
        if (Physics.SphereCast(this.transform.position, 0.5f,-(Vector3.forward), out hit[1]))
        {
            Debug.Log("��냌�C" + hit[1].point);
            if (hit[1].point.z >=this.transform.position.z-player_Radius)
            {
                vertical = Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1.0f);
                Debug.Log("�ǔ���i���j" + this.transform.position);
            }
        }
        if (Physics.SphereCast(this.transform.position, 0.5f, Vector3.right, out hit[2]))
        {
            Debug.Log("�E�ʃ��C" + hit[2].point);
            if (hit[2].point.x <= this.transform.position.x+player_Radius)
            {
                horizontal = Mathf.Clamp(Input.GetAxis("Horizontal"), -1.0f, 0f);
                Debug.Log("�ǔ���i�E�ʁj" + this.transform.position);
            }
            
        }
        if (Physics.SphereCast(this.transform.position, 0.5f, -(Vector3.right), out hit[3]))
        {
           Debug.Log("���ʃ��C" + hit[3].point);
           if (hit[3].point.x >= this.transform.position.x-player_Radius)
           {
             horizontal = Mathf.Clamp(Input.GetAxis("Horizontal"), 0f, 1.0f);
             Debug.Log("�ǔ���i���ʁj" + this.transform.position);
           }
         }
        this.transform.Translate(horizontal*(moveSpeed*Time.deltaTime),0,vertical*(moveSpeed*Time.deltaTime));
    } 
}
    