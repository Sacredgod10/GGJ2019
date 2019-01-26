using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject player;
    public GameObject bottleNote;
    public GameObject showerNote;
    public Camera gameCamera;
    public bool itemInHand = false;
    public float lerpSpeed = 1;

    private bool lerping;
    private Vector3 startPosition, endPosition;

    public enum InteractionType
    {
        REMOTE,
        PILLOW,
        MONITOR,
        PAPER,
        SHOWER,
        BOTTLE
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
                    Destroy(gameObject);
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
            case InteractionType.PAPER:
                {
                    Destroy(gameObject);
                    break;
                }
            case InteractionType.SHOWER:
                {
                    StopAllCoroutines();
                    StartCoroutine(ShowerStream());
                    break;
                }
            case InteractionType.BOTTLE:
                {
                    GameObject note = Instantiate(bottleNote, gameObject.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
                    note.transform.eulerAngles += new Vector3(90, 0, 0);
                    Destroy(gameObject);
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

    public IEnumerator ShowerStream()
    {
        // poor water
        Debug.Log("Water is pooring you just cant see it");
        yield return new WaitForSeconds(10);
        // Stop pooring water
        Instantiate(showerNote, gameObject.transform.position, Quaternion.identity);
    }
}
