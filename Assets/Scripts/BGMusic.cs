using UnityEngine;
using System.Collections;

public class BGMusic : MonoBehaviour {

	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource>();
		print(audio);
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}
