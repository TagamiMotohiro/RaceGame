using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Line : MonoBehaviour
{
    [SerializeField]
    float rayLength;
    [SerializeField]
    RaceManeger maneger;
    [SerializeField]
    LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.right*rayLength);
        RaycastHit hit;
        Debug.DrawLine(transform.position,transform.position+transform.right*10);
        if (Physics.Raycast(ray, out hit,playerMask))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                maneger.Lap();
                this.gameObject.SetActive(false);
            }
        }
    }
}
