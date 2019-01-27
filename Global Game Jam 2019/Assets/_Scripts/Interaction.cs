using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public CharacteristicsAndData characteristicsAndData;
    public GameObject player;
    public GameObject noteToDrop;
    public ParticleSystem smokeGen;
    public GameObject waterGroup1, waterGroup2;
    public Camera gameCamera;
    public bool itemInHand = false;
    public float lerpSpeed = 1;
    public Vector3 placeToLand;
    public int roomNr;
    bool doorOpen = false;

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
        AFZUIGKAP,
        DOOR,
        GOODPRACTISE,
        BADPRACTISE
    }

    public InteractionType interactionType;

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.PILLOW:
                {
                    Destroy(gameObject);
                    ActivateFlash(true);
                    break;
                }
            case InteractionType.REMOTE:
                {
                    ActivateFlash(true);
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
                    ActivateFlash(true);
                    Destroy(gameObject);
                    break;
                }
            case InteractionType.SHOWER:
                {
                    StopAllCoroutines();
                    GetComponent<SoundController>().PlaySoundAtLocation();
                    ActivateFlash(true);
                    StartCoroutine(ShowerStream());
                    break;
                }
            case InteractionType.BOTTLE:
                {
                    GameObject note = Instantiate(noteToDrop, gameObject.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
                    note.transform.eulerAngles += new Vector3(90, 0, 0);
                    ActivateFlash(true);
                    Destroy(gameObject);
                    break;
                }
            case InteractionType.POT:
                {
                    if (characteristicsAndData.smoking == false)
                    {
                        StartCoroutine(WasteEnergy());
                    }
                    smokeGen.gameObject.SetActive(true);
                    characteristicsAndData.smoking = true;
                    GetComponent<SoundController>().PlaySoundAtLocation();
                    roomNr = 1;
                    break;
                }
            case InteractionType.AFZUIGKAP:
                {
                    StopAllCoroutines();
                    GetComponent<SoundController>().PlaySoundAtLocation();
                    StopCoroutine(WasteEnergy());
                    if (characteristicsAndData.smoking)
                    {
                        var main = smokeGen.main;
                        main.startLifetime = 1.5f;
                        StartCoroutine(Steam());
                    }
                    break;
                }
            case InteractionType.DOOR:
                {
                    if (!doorOpen)
                    {
                        doorOpen = true;
                        gameObject.transform.localEulerAngles = new Vector3(0, -90, 0);
                    }
                    else if (doorOpen)
                    {
                        doorOpen = false;
                        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    break;
                }
            case InteractionType.GOODPRACTISE:
                {
                    ActivateFlash(true);
                    break;
                }
            case InteractionType.BADPRACTISE:
                {
                    ActivateFlash(false);
                    break;
                }
        }
    }

    public void ActivateFlash(bool good)
    {
        var flash = GameObject.Find("Canvas").GetComponent<Flash>();
        flash.good = good;
        flash.FlashIn();
    }

    public IEnumerator ShowerStream()
    {
        StartCoroutine(WaterStream());
        yield return new WaitForSeconds(10);
        // Stop pooring water
        Instantiate(noteToDrop, gameObject.transform.position, Quaternion.identity);
    }

    public IEnumerator WaterStream()
    {
        bool active = true;
        for (int i = 0; i < 20; i++)
        {
            waterGroup1.SetActive(active);
            waterGroup2.SetActive(!active);
            active = !active;
            yield return new WaitForSeconds(0.5f); 
        }
        waterGroup2.SetActive(false);
        waterGroup1.SetActive(false);
        GetComponent<SoundController>().FadeOut();
    }

    public IEnumerator Steam()
    {
        yield return new WaitForSeconds(6);
        ActivateFlash(true);
        var main = smokeGen.main;
        main.startLifetime = 0.1f;
        SoundController pot = GameObject.Find("POT").GetComponent<SoundController>();
        pot.FadeOut();
        GetComponent<SoundController>().FadeOut();
        yield return new WaitForSeconds(.5f);
        main.startLifetime = 3f;
        characteristicsAndData.smoking = false;
        smokeGen.gameObject.SetActive(false);
        Instantiate(noteToDrop, player.transform.position + new Vector3(0, 15, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(noteToDrop, player.transform.position + new Vector3(0, 15, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(noteToDrop, player.transform.position + new Vector3(0, 15, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator WasteEnergy()
    {
        yield return new WaitForSeconds(10);

        if (characteristicsAndData.smoking == true)
        {
            if (CharacteristicsAndData.Instance.happyPercentages[roomNr] >= 20)
            {
                CharacteristicsAndData.Instance.happyPercentages[roomNr] -= 20;
            }
            else CharacteristicsAndData.Instance.happyPercentages[roomNr] = 0;

            ActivateFlash(false);
            StartCoroutine(WasteEnergy());
        }
    }
}
