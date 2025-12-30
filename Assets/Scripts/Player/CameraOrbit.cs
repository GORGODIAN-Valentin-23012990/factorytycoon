using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("Cible")]
    public Transform target;

    [Header("Orbite")]
    public float distance = 6f;
    public float minDistance = 3f;
    public float maxDistance = 12f;
    public float xSpeed = 180f;
    public float ySpeed = 120f;
    public float yMin = -20f;
    public float yMax = 60f;

    [Header("Offset vertical en plus (au-dessus du centre)")]
    public float lookUpOffset = 0.2f;

    float yaw, pitch;

    void Start(){
        if (!target){ Debug.LogWarning("SimpleOrbitAutoCenter: assigne target (Player)."); return; }
        var e = transform.eulerAngles;
        yaw = e.y;
        pitch = Mathf.Clamp(e.x, yMin, yMax);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate(){
        if (!target) return;

        // rotation souris
        yaw   += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        pitch  = Mathf.Clamp(pitch, yMin, yMax);

        // zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.0001f)
            distance = Mathf.Clamp(distance - scroll * 5f, minDistance, maxDistance);

        Vector3 focus = GetTargetCenter(target);
        focus += Vector3.up * lookUpOffset;

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 pos = focus - rot * Vector3.forward * distance;

        transform.SetPositionAndRotation(pos, rot);
        transform.LookAt(focus);
    }

    Vector3 GetTargetCenter(Transform t){
        Collider col = t.GetComponentInChildren<Collider>();
        if (col) return col.bounds.center;

        Renderer rend = t.GetComponentInChildren<Renderer>();
        if (rend) return rend.bounds.center;

        return t.position;
    }
}