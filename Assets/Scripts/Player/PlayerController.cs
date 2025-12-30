using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 6f;
    public float maxSlopeAngle = 50f;

    Rigidbody rb;
    Vector3 input;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update(){
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector3(h, 0f, v).normalized;
    }

    void FixedUpdate(){
        var cam = Camera.main ? Camera.main.transform : null;
        Vector3 fwd = cam ? Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized : Vector3.forward;
        Vector3 right = cam ? Vector3.ProjectOnPlane(cam.right,   Vector3.up).normalized : Vector3.right;

        Vector3 moveWorld = (right * input.x + fwd * input.z) * moveSpeed;
        Vector3 target = new Vector3(moveWorld.x, rb.velocity.y, moveWorld.z);
        rb.velocity = target;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.sqrMagnitude > 0.01f){
            Quaternion look = Quaternion.LookRotation(flatVel, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, 0.2f));
        }
    }
}