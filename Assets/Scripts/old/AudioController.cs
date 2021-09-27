using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	
	public AudioSource currentSound;
	
	public void PlaySound() {
		currentSound.Play();
	}
	
	public void StopSound() {
		currentSound.Stop();
	}
}
