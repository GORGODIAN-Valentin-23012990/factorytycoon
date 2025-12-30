using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform spawnPoint;
    public float rate = 1f;

    float t;

    void Update(){
        if (!coinPrefab || !spawnPoint) return;
        t += Time.deltaTime;
        if (t >= 1f / Mathf.Max(0.01f, rate)){
            t = 0f;
            Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}