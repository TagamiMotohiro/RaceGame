using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Reload : MonoBehaviour
{
    public GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.right);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                Manager.SendMessage("Reload");
                Debug.Log("Reloaded");
            }
        }
    }
}
