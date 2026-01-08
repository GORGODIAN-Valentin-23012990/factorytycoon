using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey;

    public void GiveKey() => hasKey = true;
    public void ConsumeKey() => hasKey = false;
}