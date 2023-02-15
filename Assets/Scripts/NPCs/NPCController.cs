using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialogue;

    public void Interact() 
    {
        Debug.Log("NPC");
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        
    }
}
