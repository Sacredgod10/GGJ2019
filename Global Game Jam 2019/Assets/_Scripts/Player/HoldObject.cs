using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public GameObject currentlyHolding;
    public GameObject placeObjectOn;
    public GameObject holderOriginalParent;
    public GameObject[] possibleHolders;
    public GameObject[] possibleInteractables;

    public void PlaceObject(GameObject gameObject)
    {
        currentlyHolding = gameObject;
        holderOriginalParent = currentlyHolding.transform.parent.gameObject;
        currentlyHolding.transform.parent = placeObjectOn.transform;
        var interactionScript = currentlyHolding.GetComponent<Interaction>();
        interactionScript.itemInHand = true;
        currentlyHolding.GetComponent<Collider>().enabled = false;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;

        if (currentlyHolding == possibleHolders[0]) // Remote
        {
            var ct = currentlyHolding.transform;
            ct.localPosition = new Vector3(0, 1, -1);
            ct.localEulerAngles = new Vector3(270, 0, 90);
            ct.localScale = new Vector3(0.3f, 0.15f, 0.05f);
        }
    }

    public void DropObject()
    {
        currentlyHolding.transform.parent = holderOriginalParent.transform;
        var interactionScript = currentlyHolding.GetComponent<Interaction>();
        interactionScript.itemInHand = false;
        currentlyHolding.GetComponent<Collider>().enabled = true;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHolding.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        currentlyHolding = null;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact(currentlyHolding);
        }
    }

    public void Interact(GameObject gameObjectInteract)
    {
        currentlyHolding = gameObjectInteract;
        if (currentlyHolding == possibleHolders[0]) // Remote
        {
            possibleInteractables[1].GetComponent<MonitorZoom>().ChangeTvState(!possibleInteractables[1].GetComponent<MonitorZoom>().isOn); // Turn on tv
            Destroy(gameObjectInteract);
            // DropObject();
        }
    }
}
