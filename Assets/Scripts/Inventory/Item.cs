using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType
    { 
        HealthPotion,
        Coins,
        Apple,
        Cloak,
        MP,
        Shield,

    
    
    
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return ItemAssets.Instance.HealthPotion;
            case ItemType.Coins: return ItemAssets.Instance.Coins;
            case ItemType.Apple: return ItemAssets.Instance.Apple;
            case ItemType.Cloak: return ItemAssets.Instance.Cloak;
            case ItemType.MP: return ItemAssets.Instance.MP;
            case ItemType.Shield: return ItemAssets.Instance.Shield;



        }
    }

    public bool isStackable() 
    {
        switch (itemType) 
        { 
            default :
            case ItemType.HealthPotion:
            case ItemType.Coins:
            case ItemType.Apple:
            case ItemType.MP: 
                return true;
            case ItemType.Shield:
            case ItemType.Cloak:
                return false;

        }

    }

}
