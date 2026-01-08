using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class BuyNextGeneratorPad : MonoBehaviour
{
    public Wallet wallet;
    public GeneratorManager manager;

    [Header("UI")]
    public GameObject promptRoot;
    public TMP_Text prompt3dText;

    public KeyCode useKey = KeyCode.E;

    bool inRange;

    void Awake(){
        GetComponent<Collider>().isTrigger = true;
        if (!wallet) wallet = FindObjectOfType<Wallet>();
        if (!manager) manager = FindObjectOfType<GeneratorManager>();
        if (promptRoot) promptRoot.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = true;
        RefreshPrompt(true);
    }

    void OnTriggerExit(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = false;
        if (promptRoot) promptRoot.gameObject.SetActive(false);
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
        if (!promptRoot || !prompt3dText || manager == null) return;

        if (manager.IsMaxed()){
            prompt3dText.text = "MAX";
            promptRoot.gameObject.SetActive(show);
            return;
        }

        int cost = manager.GetNextCost();
        prompt3dText.text = $"{useKey} — Buy ({cost})";
        if (show) promptRoot.gameObject.SetActive(true);
    }
}