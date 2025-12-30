using UnityEngine;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    public Wallet wallet;
    public Text coinsText;

    void Start(){
        if (wallet) wallet.onCoinsChanged.AddListener(UpdateCoins);
        UpdateCoins(wallet ? wallet.coins : 0);
    }

    void UpdateCoins(int value){
        if (coinsText) coinsText.text = "Coins: " + value;
    }
}