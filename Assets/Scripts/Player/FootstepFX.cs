using UnityEngine;

public class FootstepFX : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody rb;
    public CapsuleCollider capsule;
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public ParticleSystem footParticles;

    [Header("Tuning")]
    public float minSpeed = 0.5f;
    public float stepIntervalWalk = 0.45f;
    public float stepIntervalRun = 0.28f;
    public float groundCheckExtra = 0.05f;

    float timer;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!capsule) capsule = GetComponent<CapsuleCollider>();
        if (!audioSource) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!rb || !capsule || footstepClips == null || footstepClips.Length == 0) return;
        
        Vector3 v = rb.velocity;
        v.y = 0f;
        float speed = v.magnitude;

        if (speed < minSpeed || !IsGrounded())
        {
            timer = 0f;
            return;
        }

        float t = Mathf.InverseLerp(minSpeed, minSpeed + 5f, speed);
        float interval = Mathf.Lerp(stepIntervalWalk, stepIntervalRun, t);

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            PlayStep();
        }
    }

    bool IsGrounded()
    {
        Vector3 center = capsule.bounds.center;
        float radius = capsule.radius * 0.95f;
        float halfHeight = Mathf.Max(capsule.height * 0.5f - radius, 0.01f);

        Vector3 top = center + Vector3.up * halfHeight;
        Vector3 bottom = center - Vector3.up * halfHeight;
        
        return Physics.CheckCapsule(top, bottom + Vector3.down * groundCheckExtra, radius, ~0, QueryTriggerInteraction.Ignore);
    }

    void PlayStep()
    {
        if (audioSource && footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(clip, 1f);
        }

        if (footParticles)
        {
            footParticles.transform.position = new Vector3(transform.position.x, capsule.bounds.min.y + 0.02f, transform.position.z);
            footParticles.Play();
        }
    }
}
