using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [Header("Références")]
    public Wallet wallet;
    public Dropper dropper;
    public GameObject promptUI;

    [Header("Paliers")]
    public int[] costs = {10, 25, 50};
    public float[] rates = {1f, 2f, 3.5f};

    public KeyCode useKey = KeyCode.E;

    int level;
    bool inRange;

    void Start(){
        Apply();
        if (promptUI) promptUI.SetActive(false);
    }

    void Apply(){
        if (!dropper) return;
        int idx = Mathf.Clamp(level, 0, rates.Length - 1);
        dropper.rate = rates[idx];
        UpdatePrompt();
    }

    void UpdatePrompt(){
        if (!promptUI) return;
        var text = promptUI.GetComponentInChildren<UnityEngine.UI.Text>();
        if (text){
            if (level >= costs.Length) text.text = "Amélioration: MAX";
            else text.text = $"E – Upgrade ({costs[level]})";
        }
    }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = true;
        if (promptUI) { UpdatePrompt(); promptUI.SetActive(true); }
    }

    void OnTriggerExit(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = false;
        if (promptUI) promptUI.SetActive(false);
    }

    void Update(){
        if (!inRange) return;
        if (level >= costs.Length) return;

        if (Input.GetKeyDown(useKey) && wallet && wallet.TrySpend(costs[level])){
            level++;
            Apply();
            if (level >= costs.Length && promptUI) UpdatePrompt();
        }
    }
}