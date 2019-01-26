using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject player;

    public bool itemInHand = false;

    public enum InteractionType
    {
        REMOTE,
        PILLOW,
        MONITOR
    }

    public InteractionType interactionType;

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.PILLOW:
                {
                    Destroy(gameObject);
                    break;
                }
            case InteractionType.REMOTE:
                {
                    var holdObject = player.GetComponent<HoldObject>();
                    holdObject.PlaceObject(gameObject);
                    break;
                }
            case InteractionType.MONITOR:
                {
                    // Zoom cam
                    break;
                }
        }
    }
}
