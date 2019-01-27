using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmmitter : MonoBehaviour {

    AudioSource myAudioEmmitter;
    public float timeToLive = 90f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeToLive -= Time.fixedDeltaTime;
        if(timeToLive <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void SetupEmitter(AudioClip audioClip)
    {
        myAudioEmmitter = GetComponent<AudioSource>();
        this.timeToLive = audioClip.length + 1;
        myAudioEmmitter.PlayOneShot(audioClip);
    }
}
