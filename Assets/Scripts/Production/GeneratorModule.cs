using UnityEngine;

public class GeneratorModule : MonoBehaviour
{
    [Header("References")]
    public Dropper dropper;
    public Conveyor conveyor;
    public Collector collector;

    [Header("Config du module")]
    public GameObject coinPrefab;
    public float dropRate = 1f;
    public float conveyorForce = 5f;

    void Reset() => AutoWire();

    void OnValidate(){
        if (!Application.isPlaying) AutoWire();
    }

    void AutoWire(){
        if (!dropper) dropper = GetComponentInChildren<Dropper>(true);
        if (!conveyor) conveyor = GetComponentInChildren<Conveyor>(true);
        if (!collector) collector = GetComponentInChildren<Collector>(true);
    }

    public void Apply()
    {
        if (dropper){
            dropper.coinPrefab = coinPrefab;
            dropper.rate = dropRate;
        }
        if (conveyor){
            conveyor.pushForce = conveyorForce;
        }
    }

    public void SetEnabled(bool enabled){
        gameObject.SetActive(enabled);
    }
}