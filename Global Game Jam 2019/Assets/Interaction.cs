using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject thisObject;

    public bool itemInHand = false;

    public enum InteractionType
    {
        REMOTE,
        PILLOW,
        MONITOR
    }

    public InteractionType interactionType;

    private void Start()
    {
        thisObject = gameObject;
    }
    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.PILLOW:
                {
                    Destroy(thisObject);
                    break;
                }
            case InteractionType.REMOTE:
                {
                    // Put in hand
                    itemInHand = true;
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
