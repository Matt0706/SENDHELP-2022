using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollABallBounce : MonoBehaviour
{

    public float bounceForce = 200.0f;
    private System.DateTime start = System.DateTime.Now;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                if(System.DateTime.Now - start > System.TimeSpan.FromSeconds(1)) {
                    Vector3 bounceDir = -playerRb.velocity;
                    playerRb.AddForce(bounceDir * bounceForce);
                }
                start = System.DateTime.Now;
            }
        }
    }
}
