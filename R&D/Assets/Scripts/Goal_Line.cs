using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Line : MonoBehaviour
{
    [SerializeField]
    float rayLength;
    [SerializeField]
    protected RaceManeger manager;
    [SerializeField]
    LayerMask PlayerLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.right * rayLength);
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, PlayerLayer))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.gameObject.tag == "Player")
            {
                LineHitCall();
            }
        }
    }
    protected virtual void LineHitCall()
    {
        Debug.Log("Lap");
        manager.Lap();
        gameObject.SetActive(false);
    }
}
