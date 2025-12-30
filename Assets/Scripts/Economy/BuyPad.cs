using UnityEngine;

public class BuyPad : MonoBehaviour
{
    [Header("Refs")]
    public Wallet wallet;
    public GameObject toActivate;
    public GameObject[] toHide;

    [Header("Buy")]
    public int cost = 10;
    public KeyCode useKey = KeyCode.E;

    [Header("UI")]
    public GameObject promptUI;

    bool inRange;
    bool bought;

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = true;
        if (promptUI) promptUI.SetActive(!bought);
    }

    void OnTriggerExit(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = false;
        if (promptUI) promptUI.SetActive(false);
    }

    void Update(){
        if (!inRange || bought) return;
        if (Input.GetKeyDown(useKey)){
            if (wallet && wallet.TrySpend(cost)){
                if (toActivate) toActivate.SetActive(true);
                if (promptUI) promptUI.SetActive(false);
                foreach (var go in toHide) if (go) go.SetActive(false);
                bought = true;
            } else {
                // TODO : Audio
            }
        }
    }
}