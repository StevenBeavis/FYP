using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]

public class Dialogue : MonoBehaviour
{
    [SerializeField] public List<string> lines;
    

    public List<string> Lines { get { return lines; } }   

   
}
