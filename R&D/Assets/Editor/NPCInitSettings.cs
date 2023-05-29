using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCInitSettings : MonoBehaviour
{
    [MenuItem("NPC/NPC�����ݒ�")]
    // Start is called before the first frame update
    public static void NPCInit()
    {
        GameObject[] NPClist = GameObject.FindGameObjectsWithTag("NPC");
        GameObject[] checkpointList = GameObject.FindGameObjectsWithTag("CheckPoint");
        //NPC�ƃ`�F�b�N�|�C���g���擾
        
        foreach (GameObject item in NPClist) {
            NPCCtrl nItem = item.GetComponent<NPCCtrl>();
            List<GameObject> followPointList = new List<GameObject>();
            nItem.AddCheckPoint(checkpointList);
            switch (nItem.GetCharacter())//���i�̒l�ɂ���ă`�F�b�N�|�C���g���̂ǂ���ڂ����邩���߂�
            {
                case NPCCtrl.NPCCharacter.InCourse:
                    for (int i = 0; i < checkpointList.Length; i++)
                    {
                        followPointList.Add(checkpointList[i]);
                    }
                    break;
                case NPCCtrl.NPCCharacter.Nomal:
                    for (int i = 0; i < checkpointList.Length; i++)
                    {
                        followPointList.Add(checkpointList[i].transform.GetChild(0).gameObject);
                    }
                    break;
                case NPCCtrl.NPCCharacter.OutCourse:
                    for (int i = 0; i < checkpointList.Length; i++)
                    {
                        followPointList.Add(checkpointList[i].transform.GetChild(1).gameObject);
                    }
                    break;
            }
            nItem.AddFollowPoint(followPointList.ToArray());
            EditorUtility.SetDirty(nItem);
        }
    }
}
