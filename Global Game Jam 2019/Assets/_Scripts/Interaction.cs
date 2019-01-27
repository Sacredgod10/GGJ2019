using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public CharacteristicsAndData characteristicsAndData;
    public GameObject player;
    public GameObject noteToDrop;
    public ParticleSystem smokeGen;
    public Camera gameCamera;
    public bool itemInHand = false;
    public float lerpSpeed = 1;
    public Vector3 placeToLand;

    private bool lerping;
    private Vector3 startPosition, endPosition;

    public enum InteractionType
    {
        REMOTE,
        PILLOW,
        MONITOR,
        PAPER,
        SHOWER,
        BOTTLE,
        POT,
        AFZUIGKAP
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
                    Destroy(gameObject);
                    break;
                }
            case InteractionType.MONITOR:
                {
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
                    GameObject note = Instantiate(noteToDrop, gameObject.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
                    note.transform.eulerAngles += new Vector3(90, 0, 0);
                    Destroy(gameObject);
                    break;
                }
            case InteractionType.POT:
                {
                    smokeGen.gameObject.SetActive(true);
                    characteristicsAndData.smoking = true;
                    break;
                }
            case InteractionType.AFZUIGKAP:
                {
                    StopAllCoroutines();
                    // Maak herrie 
                    if (characteristicsAndData.smoking)
                    {
                        var main = smokeGen.main;
                        main.startLifetime = 1.5f;
                        StartCoroutine(Steam());
                    }
                    break;
                }
        }
    }

    public IEnumerator ShowerStream()
    {
        // poor water
        Debug.Log("Water is pooring you just cant see it");
        yield return new WaitForSeconds(10);
        // Stop pooring water
        Instantiate(noteToDrop, gameObject.transform.position, Quaternion.identity);
    }

    public IEnumerator Steam()
    {
        yield return new WaitForSeconds(6);
        var main = smokeGen.main;
        main.startLifetime = 0.1f;
        yield return new WaitForSeconds(1);
        main.startLifetime = 3f;
        characteristicsAndData.smoking = false;
        smokeGen.gameObject.SetActive(false);
        Instantiate(noteToDrop, player.transform.position + new Vector3(0, 20, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(noteToDrop, player.transform.position + new Vector3(0, 20, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(noteToDrop, player.transform.position + new Vector3(0, 20, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }
}
