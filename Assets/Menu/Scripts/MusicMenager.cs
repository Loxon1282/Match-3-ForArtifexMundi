using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenager : MonoBehaviour {

	static bool AudioBegin = false; 
	void Awake()
	{
		GetComponent<AudioSource> ().Stop ();
		if (!AudioBegin) {
			GetComponent<AudioSource>().Play ();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
		} 

	}
}
