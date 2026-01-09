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
    public KeyCode claimKey = KeyCode.E;

    [Header("Comportement")]
    public bool autoOpen = true;
    public bool autoCloseOnExit = true;

    bool inRange;
    bool claimed;
    bool panelOpen;

    PlayerInventory currentInv;

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;

        if (chestPanel) chestPanel.SetActive(false);

        if (!wallet) wallet = FindObjectOfType<Wallet>();

        if (claimButton)
        {
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(Claim);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inRange = true;
        currentInv = other.GetComponentInParent<PlayerInventory>();

        if (!autoOpen) return;
        TryOpenPanel();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inRange = false;
        currentInv = null;

        if (autoCloseOnExit) ClosePanel();
    }

    void Update()
    {
        if (panelOpen && !claimed && Input.GetKeyDown(claimKey))
        {
            Claim();
        }
    }

    void TryOpenPanel()
    {
        if (claimed) return;
        if (!inRange) return;
        if (!currentInv || !currentInv.hasKey) return;

        OpenPanel();
    }

    void OpenPanel()
    {
        if (!chestPanel) return;

        chestPanel.SetActive(true);
        panelOpen = true;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePanel()
    {
        if (chestPanel) chestPanel.SetActive(false);
        panelOpen = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Claim()
    {
        if (claimed) return;
        if (!currentInv || !currentInv.hasKey) return;

        if (wallet) wallet.Add(rewardCoins);
        currentInv.ConsumeKey();

        claimed = true;

        ClosePanel();

        gameObject.SetActive(false);
    }
}
