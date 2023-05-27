using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCInitSettings : MonoBehaviour
{
    [MenuItem("NPC/NPC初期設定")]
    // Start is called before the first frame update
    public static void NPCInit()
    {
        GameObject[] NPClist = GameObject.FindGameObjectsWithTag("NPC");
        List<GameObject> checkpointList = new List<GameObject>();
        for (int i = 0; i < 25; i++) {
            checkpointList.Add(GameObject.Find("Point (" + i.ToString("D2") + ")"));
        }
        foreach (GameObject item in NPClist) {
            NPCCtrl nItem = item.GetComponent<NPCCtrl>();
            List<GameObject> followPointList = new List<GameObject>();
            nItem.AddCheckPoint(checkpointList.ToArray());
            switch (nItem.GetCharacter())//性格の値によってコース取りを変化させる
            {
                case NPCCtrl.NPCCharacter.InCourse:
                    for (int i = 0; i < checkpointList.Count; i++)
                    {
                        followPointList.Add(GameObject.Find("Point (" + i.ToString("D2") + ")"));
                    }
                    break;
                case NPCCtrl.NPCCharacter.Nomal:
                    for (int i = 0; i < checkpointList.Count; i++)
                    {
                        followPointList.Add(GameObject.Find("Point (" + i.ToString("D2") + ")").transform.GetChild(0).gameObject);
                    }
                    break;
                case NPCCtrl.NPCCharacter.OutCourse:
                    for (int i = 0; i < checkpointList.Count; i++)
                    {
                        followPointList.Add(GameObject.Find("Point (" + i.ToString("D2") + ")").transform.GetChild(1).gameObject);
                    }
                    break;
            }
            nItem.AddFollowPoint(followPointList.ToArray());
            EditorUtility.SetDirty(nItem);
        }
    }
}
