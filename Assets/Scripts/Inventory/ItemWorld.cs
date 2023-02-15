using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    { 
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorldPF, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);


        return itemWorld;

    }

    /*public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        SpawnItemWorld()
    }*/

    

    
    private TextMeshPro textMeshPro;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(transform.Find("Text").GetComponent<TextMeshPro>());
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        
        
    }

    
    public void SetItem(Item item) 
    { 
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            Debug.Log(textMeshPro);
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            Debug.Log(textMeshPro);
            textMeshPro.SetText(""); 
        }
        
    }

    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    { 
        Destroy(gameObject);
    }


}
