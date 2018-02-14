using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource backgroundSound = SoundManager.GetSound("desertloop",false);
		backgroundSound.loop = true;
		backgroundSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
