using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject player;
    public Camera gameCamera;
    public bool itemInHand = false;
    public float lerpSpeed = 1;

    private bool lerping;
    private Vector3 startPosition, endPosition;

    public enum InteractionType
    {
        REMOTE,
        PILLOW,
        MONITOR
    }

    public InteractionType interactionType;

    private void Update()
    {
        if(lerping)
        {
            LerpCamera(startPosition, endPosition, lerpSpeed);
        }
    }

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
                    GameObject cameraPerspective = GameObject.Find("MonitorCamPerspective");
                    lerping = true;
                    startPosition = gameCamera.transform.position;
                    endPosition = cameraPerspective.transform.position;
                    gameObject.GetComponent<MonitorInput>().enabled = true;
                    break;
                }
        }
    }

    public void LerpCamera(Vector3 curPos, Vector3 endPos, float speed)
    {
        gameCamera.transform.position = Vector3.Lerp(curPos, endPos, speed * Time.deltaTime);
        startPosition = gameCamera.transform.position;
        if(Vector3.Distance(curPos, endPos) < 0.01f)
        {
            lerping = false;
        }
    }
}
