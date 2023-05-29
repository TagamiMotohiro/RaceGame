using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Reload : MonoBehaviour //2023îN5åé28ì˙Ç…èCê≥ÇçsÇ¢Ç‹ÇµÇΩÅB
{
    [SerializeField]
    float rayLength;
    [SerializeField]
    RaceManeger manager;
    [SerializeField]
    LayerMask PlayerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.right*rayLength);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.right);
        Debug.DrawLine(transform.position, transform.position + transform.right * 10);
        if (Physics.Raycast(ray, out hit,PlayerLayer))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                manager.SetCanGall();
                Debug.Log("Reloaded");
            }
        }
    }
}
