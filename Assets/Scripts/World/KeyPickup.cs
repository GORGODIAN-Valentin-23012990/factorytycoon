using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyPickup : MonoBehaviour
{
    public GameObject pickupFX;
    public AudioSource audioSource;
    public AudioClip sfxPickup;

    void Awake(){
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        
        var inv = other.GetComponentInParent<PlayerInventory>();
        if (!inv) return;

        inv.GiveKey();

        if (pickupFX) Instantiate(pickupFX, transform.position, Quaternion.identity);
        if (audioSource && sfxPickup) audioSource.PlayOneShot(sfxPickup);

        Destroy(gameObject);
    }
}