using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public GameObject soundEmmitterPrefab;
    public AudioClip soundFile;
    public GameObject spawnedEm;
    public Vector3 offset;
    public int activePlayers;

    public void PlaySoundAtLocation()
    {
        if (activePlayers == 0)
        {
            spawnedEm = Instantiate(soundEmmitterPrefab, offset, Quaternion.identity);
            spawnedEm.GetComponent<SoundEmmitter>().SetupEmitter(soundFile);
            activePlayers++;
        }
    }

    private void Update()
    {
        if (!spawnedEm)
        {
            activePlayers = 0;
        }
    }

    public void FadeOut()
    {
        var audioSource = spawnedEm.GetComponent<AudioSource>();
        audioSource.Stop();
    }
}
