using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Conveyor : MonoBehaviour
{
    public float pushForce = 5f;
    public Vector3 localDirection = Vector3.right;

    void Awake(){
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerStay(Collider other){
        if (other.attachedRigidbody){
            Vector3 dir = transform.TransformDirection(localDirection.normalized);
            other.attachedRigidbody.AddForce(dir * pushForce, ForceMode.Acceleration);
        }
    }
}