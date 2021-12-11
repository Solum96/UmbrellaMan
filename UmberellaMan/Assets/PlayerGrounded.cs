using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground")/* && player.state == PlayerState.airborne*/)
        {
            //player.state = PlayerState.grounded;
            player.animator.SetBool("isAirborne", false);
        }
    }
}
