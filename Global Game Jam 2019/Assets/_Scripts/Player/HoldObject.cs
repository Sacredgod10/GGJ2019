using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public GameObject currentlyHolding;
    public GameObject placeObjectOn;

    public void PlaceObject(GameObject gameObject)
    {
        currentlyHolding = gameObject;
        currentlyHolding.transform.parent = placeObjectOn.transform;
        var interactionScript = currentlyHolding.GetComponent<Interaction>();
        interactionScript.itemInHand = true;
        currentlyHolding.GetComponent<Collider>().enabled = false;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void DropObject()
    {
        currentlyHolding.transform.parent = null;
        var interactionScript = currentlyHolding.GetComponent<Interaction>();
        interactionScript.itemInHand = false;
        currentlyHolding.GetComponent<Collider>().enabled = true;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHolding = null;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentlyHolding)
        {
            Interact();
        }
    }

    public void Interact()
    {
        // Activate
    }
}
