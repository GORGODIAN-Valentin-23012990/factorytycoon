using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ChestInteract : MonoBehaviour
{
    [Header("Références")]
    public Wallet wallet;
    public GameObject chestPanel;
    public Button claimButton;
    public TMP_Text infoText;

    [Header("Récompense")]
    public int rewardCoins = 200;

    [Header("Input")]
    public KeyCode openKey = KeyCode.E;

    bool inRange;
    PlayerInventory currentInv;
    bool claimed;

    void Awake(){
        GetComponent<Collider>().isTrigger = true;

        if (chestPanel) chestPanel.SetActive(false);

        if (!wallet) wallet = FindObjectOfType<Wallet>();

        if (claimButton){
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(Claim);
        }
    }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = true;
        currentInv = other.GetComponentInParent<PlayerInventory>();
    }

    void OnTriggerExit(Collider other){
        if (!other.CompareTag("Player")) return;
        inRange = false;
        currentInv = null;
        if (chestPanel) chestPanel.SetActive(false);
    }

    void Update(){
        if (!inRange || claimed) return;
        if (!currentInv || !currentInv.hasKey) return;

        if (Input.GetKeyDown(openKey)){
            OpenPanel();
        }
    }

    void OpenPanel(){
        if (!chestPanel) return;

        if (infoText){
            infoText.text = $"Récompense : +{rewardCoins} coins\nClique sur Récupérer.";
        }

        chestPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Claim(){
        if (claimed) return;
        if (!currentInv || !currentInv.hasKey) return;
        
        if (wallet) wallet.Add(rewardCoins);
        currentInv.ConsumeKey();

        claimed = true;

        if (chestPanel) chestPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
