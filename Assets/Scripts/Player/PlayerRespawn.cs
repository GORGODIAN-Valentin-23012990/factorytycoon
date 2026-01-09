using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnYOffset = 0.2f;

    Rigidbody rb;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        if (!respawnPoint) return;

        if (rb){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Vector3 pos = respawnPoint.position + Vector3.up * respawnYOffset;
        transform.SetPositionAndRotation(pos, respawnPoint.rotation);
    }
}