using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSlider : MonoBehaviour
{
    public AirRideMove moveScript;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�`���[�W�^���N���̒l��0~1�܂ł̊Ԃɐ��K������
        //���K���̎�
        //�i�[�����f�[�^��X�A�l���x�Ƃ����ꍇ
        //X=X-xmin/xmax-xmin
        //����̏ꍇ�Œ�l�͂�������O�Ȃ̂Ō��Z���s���K�v�͂Ȃ�
        //����ă`���[�W�^���N/�`���[�W��max�l�Ő��K�������l���Ƃ邱�Ƃ��ł���
        image.fillAmount = moveScript.GetCharge_Tank() / moveScript.GetMaxCharge();
    }
}
