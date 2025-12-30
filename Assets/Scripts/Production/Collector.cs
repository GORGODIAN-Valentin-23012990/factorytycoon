using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    public Wallet wallet;

    void Awake(){
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other){
        var coin = other.GetComponent<Coin>();
        if (!coin) return;

        if (wallet) wallet.Add(coin.value);
        Destroy(other.gameObject);
    }
    public void SetWallet(Wallet w) => wallet = w;
}