using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	AudioSource SoundPlayer;
	public static SoundManager instance;

	void Awake(){
		if (SoundManager.instance == null)
			SoundManager.instance = this;
	}
	void Start () {
		SoundPlayer = GetComponent<AudioSource> ();
	}
	public void PlayBGM(string name){
		if (SoundPlayer.clip == null || SoundPlayer.clip.name != name) {
			AudioClip BGM = Resources.Load<AudioClip>("BGMs/" + name);
			SoundPlayer.clip = BGM;
		}
		if (!SoundPlayer.isPlaying)
			SoundPlayer.Play ();
		SoundPlayer.loop = true;
	}
	public void StopBGM(){
		SoundPlayer.Stop ();
	}
	public void EndBGM(){
		StopBGM ();
		SoundPlayer.clip = null;
	}
	public void PlaySE(string name){
		AudioClip SE = Resources.Load<AudioClip>("SEs/" + name);
		SoundPlayer.PlayOneShot (SE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
