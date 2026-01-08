using UnityEngine;

public class FloatingPickup : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float floatAmplitude = 0.15f;
    public float floatFrequency = 2f;

    Vector3 startPos;

    void Start(){
        startPos = transform.position;
    }

    void Update(){
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);

        float y = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, y, 0f);
    }
}