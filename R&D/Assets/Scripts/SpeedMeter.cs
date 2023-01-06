using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    public GameObject player;
    public AirRideMove AirRideMove;
    public TextMeshProUGUI this_Text;
    // Start is called before the first frame update
    void Start()
    {
        AirRideMove = player.GetComponent<AirRideMove>(); 
    }

    // Update is called once per frame
    void Update()
    {;
        this_Text.text = AirRideMove.GetVelocity_Magnitude().ToString("00");
    }
}
