using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaterKillZone : MonoBehaviour
{
    public int coinPenalty = 25;

    public Wallet wallet;
    public ScreenFlash screenFlash;

    [Header("Anti multi-trigger")]
    public float cooldown = 1.0f;
    float nextAllowedTime;

    void Awake(){
        GetComponent<Collider>().isTrigger = true;

        if (!wallet) wallet = FindObjectOfType<Wallet>();
        if (!screenFlash) screenFlash = FindObjectOfType<ScreenFlash>();
    }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        if (Time.time < nextAllowedTime) return;

        nextAllowedTime = Time.time + cooldown;
        
        if (wallet){
            wallet.TrySpend(coinPenalty);
        }
        
        if (screenFlash) screenFlash.FlashRed();
        
        var respawn = other.GetComponentInParent<PlayerRespawn>();
        if (respawn) respawn.Respawn();
    }
}