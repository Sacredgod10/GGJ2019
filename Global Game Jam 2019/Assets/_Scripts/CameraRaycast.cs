using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private GameObject target;
    public RaycastHit raycastHit;
    public Collider raycastHitCollider;

    public GameObject lastObjectSeen;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        int layerMask = 1 << 9;
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<InteractableObject>())
            {
                lastObjectSeen = hit.collider.gameObject;
            }
            if (lastObjectSeen)
            {
                var interactAble = lastObjectSeen.transform.gameObject.GetComponent<InteractableObject>();
                interactAble.shineCounter++;
                interactAble.beingLookedAt = true;
            }
        }
        else
        {
            if (lastObjectSeen)
            {
                var interactAble = lastObjectSeen.transform.gameObject.GetComponent<InteractableObject>();
                interactAble.shineCounter = 0;
                interactAble.beingLookedAt = false;
            }
        }
    }
}
