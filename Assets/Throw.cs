using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public string text;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision made: " + gameObject.name + collision.gameObject.name);
    }
}
