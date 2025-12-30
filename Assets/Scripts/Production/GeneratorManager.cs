using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public Wallet wallet;

    [Header("Modules dans l’ordre de déblocage")]
    public GeneratorModule[] modules;

    [Header("Tiers (même taille que modules)")]
    public GameObject[] coinTiers;
    public float[] dropRates;
    public float[] conveyorForces;

    [Header("Coûts de déblocage")]
    public int[] unlockCosts;

    int nextIndex = 0;

    void Awake(){
        if (!wallet) wallet = FindObjectOfType<Wallet>();

        foreach (var m in modules)
            if (m) m.SetEnabled(false);
    }

    public bool IsMaxed() => nextIndex >= modules.Length;

    public int GetNextCost() => IsMaxed() ? 0 : unlockCosts[nextIndex];

    public void UnlockNext()
    {
        if (IsMaxed()) return;

        var m = modules[nextIndex];
        if (!m) { nextIndex++; return; }

        m.coinPrefab = coinTiers[Mathf.Clamp(nextIndex, 0, coinTiers.Length - 1)];
        m.dropRate   = dropRates[Mathf.Clamp(nextIndex, 0, dropRates.Length - 1)];
        m.conveyorForce = conveyorForces[Mathf.Clamp(nextIndex, 0, conveyorForces.Length - 1)];

        if (m.collector) m.collector.SetWallet(wallet);

        m.Apply();
        m.SetEnabled(true);

        nextIndex++;
    }
}