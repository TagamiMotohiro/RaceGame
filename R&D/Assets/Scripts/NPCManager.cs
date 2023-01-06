using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<GameObject> NPC = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<NPC.Count;i++)
        {
            //NPC[i].SendMessage("GetNPCCharacter",i.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
