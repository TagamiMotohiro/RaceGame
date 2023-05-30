using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerawork : MonoBehaviour
{
    public GameObject camera_Pos;
    public Transform player;
    Vector3 camera_Velocity;
    public float camera_Speed;
    float t=0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        camera_Pos = GameObject.Find("camera_pos");
        camera_Velocity = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position,camera_Pos.transform.position,ref camera_Velocity,camera_Speed*Time.deltaTime);
        if (/*Quaternion.LookRotation(player.transform.position-this.transform.position).eulerAngles.magnitude<0.1f*/true)//カメラがプレイヤーからずれていたら
        {
            camerarotate();
        }
		//this.transform.rotation=Quaternion.FromToRotation(transform.forward,player.transform.position);
		//this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(player.transform.position-this.transform.position), 180.0f * Time.deltaTime);
    }
    void camerarotate()
    { 
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(player.transform.position-this.transform.position),t);
        t += Time.deltaTime;
    }
}
