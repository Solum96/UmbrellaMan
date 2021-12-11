using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windtunnel : MonoBehaviour
{
    public float liftSpeed = 1;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            if(player.umbrella.State == UmbrellaState.open)
            {
                var rb = other.GetComponentInParent<Rigidbody>();
                Vector3 vel = rb.velocity;
                vel.y = liftSpeed;
                rb.AddForce(vel, ForceMode.VelocityChange);
            }
        }
    }
}
