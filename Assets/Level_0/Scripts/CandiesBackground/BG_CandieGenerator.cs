using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_CandieGenerator : MonoBehaviour {

	public float quantity=20;
	public GameObject bgCandie;



	void Start () {
		//InvokeRepeating ("GenerateCandie", quantity, 0.1f);
		StartCoroutine(Gener());
	}
	
	void GenerateCandie(){
		Instantiate (bgCandie);
	}
	IEnumerator Gener(){
		for (;;) {
			Instantiate (bgCandie);
			yield return new WaitForSeconds (0.2f);
		}

	}
}
