using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TeleporterPad : MonoBehaviour
{
    [Header("Lien")]
    public TeleporterPad linkedPad;

    public Transform destinationPoint;

    [Header("Settings")]
    public float cooldownSeconds = 0.5f;

    public KeyCode requireKey = KeyCode.None;

    [Header("FX")]
    public GameObject fxOnTeleport;
    public AudioSource audioSource;
    public AudioClip sfxTeleport;

    void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;

        if (!destinationPoint)
            destinationPoint = transform;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!linkedPad) return;
        
        var playerTp = other.GetComponentInParent<PlayerTeleportState>();
        if (!playerTp) playerTp = other.gameObject.AddComponent<PlayerTeleportState>();

        if (Time.time < playerTp.nextAllowedTeleportTime) return;
        
        if (requireKey != KeyCode.None && !Input.GetKeyDown(requireKey)) return;

        Teleport(other.transform, playerTp);
    }

    void Teleport(Transform player, PlayerTeleportState state)
    {
        PlayFxAt(player.position);
        
        Vector3 destPos = linkedPad.destinationPoint ? linkedPad.destinationPoint.position : linkedPad.transform.position;
        Quaternion destRot = linkedPad.destinationPoint ? linkedPad.destinationPoint.rotation : linkedPad.transform.rotation;
        
        Transform root = player;
        var rb = player.GetComponentInParent<Rigidbody>();
        if (rb) root = rb.transform;
        
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        root.SetPositionAndRotation(destPos, destRot);
        
        PlayFxAt(destPos);
        
        state.nextAllowedTeleportTime = Time.time + cooldownSeconds;
    }

    void PlayFxAt(Vector3 pos)
    {
        if (fxOnTeleport) Instantiate(fxOnTeleport, pos, Quaternion.identity);
        if (audioSource && sfxTeleport) audioSource.PlayOneShot(sfxTeleport);
    }
}

public class PlayerTeleportState : MonoBehaviour
{
    public float nextAllowedTeleportTime = 0f;
}
