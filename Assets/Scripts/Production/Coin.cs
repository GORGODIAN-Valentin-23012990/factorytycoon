using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Coin : MonoBehaviour
{
    public int value = 1;
    public float spinSpeed = 180f;

    void Awake(){
        var col = GetComponent<Collider>();
        col.isTrigger = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    void Update(){
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
}