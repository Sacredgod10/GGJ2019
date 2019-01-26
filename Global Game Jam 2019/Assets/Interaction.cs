using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject thisObject;

    private void Start()
    {
        thisObject = gameObject;
    }
    public void Interact()
    {
        Debug.Log("Interacting with " + thisObject.name);
        //Do stuff
    }
}
