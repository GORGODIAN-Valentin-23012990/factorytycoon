using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class BuyNextGeneratorPad : MonoBehaviour
{
    public Wallet wallet;
    public GeneratorManager manager;

    [Header("UI (optionnel)")]
    public TMP_Text promptText;

    public KeyCode useKey = KeyCode.E;

    bool inRange;

    void Awake(){
        GetComponent<Collider>().isTrigger = true;
        if (!wallet) wallet = FindObjectOfType<Wallet>();
        if (!manager) manager = FindObjectOfType<GeneratorManager>();
        if (promptText) promptText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = true;
        RefreshPrompt(true);
    }

    void OnTriggerExit(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = false;
        if (promptText) promptText.gameObject.SetActive(false);
    }

    void Update(){
        if (!inRange || wallet == null || manager == null) return;

        RefreshPrompt(false);

        if (Input.GetKeyDown(useKey)){
            if (manager.IsMaxed()) return;

            int cost = manager.GetNextCost();
            if (wallet.TrySpend(cost)){
                manager.UnlockNext();
                RefreshPrompt(false);
            }
        }
    }

    void RefreshPrompt(bool show){
        if (!promptText || manager == null) return;

        if (manager.IsMaxed()){
            promptText.text = "Générateur: MAX";
            promptText.gameObject.SetActive(show);
            return;
        }

        int cost = manager.GetNextCost();
        promptText.text = $"E — Acheter générateur ({cost})";
        if (show) promptText.gameObject.SetActive(true);
    }
}