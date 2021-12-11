using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windtunnel : MonoBehaviour
{
    public float liftSpeed = 1;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if(player.umbrella.State == UmbrellaState.open)
            {
                var rb = other.GetComponent<CharacterController>();
                Vector3 vel = rb.velocity;
                vel.y = liftSpeed;
                rb.SimpleMove(vel);
            }
        }
    }
}
