using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform ItemWorldPF;

    public Sprite HealthPotion;
    public Sprite Coins;
    public Sprite Apple;
    public Sprite Cloak;
    public Sprite MP;
    public Sprite Shield;
}
