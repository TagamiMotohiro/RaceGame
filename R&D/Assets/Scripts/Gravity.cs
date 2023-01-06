using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    float height;
    bool start;
    bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
        height = this.transform.localScale.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetIsGround()
    {
        return isGround;
    }
}
