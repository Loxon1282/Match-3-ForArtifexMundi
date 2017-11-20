using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadLifeOfParticle : MonoBehaviour {



// 				(∩︵∩) 			This is a sad particle.
//	 He is sad, because he realized that his life is coming to an end.
	public float particleLifeTime = 1.0f;
//		^
//		|
//		----	You can help him live longer


	int mentalToughnessOfParticle;	// His only a little particle...

	void Start () {
		
		mentalToughnessOfParticle = Random.Range (0, 5);	//Not everyone can stand this pressure

	}

	void Update () {
		particleLifeTime -= Time.deltaTime;				// Tic tac...


		if (mentalToughnessOfParticle < 3) {
			if (particleLifeTime < 0.1f)
				KillParticle ();				// 			/'̿'̿ ̿ ̿̿ ̿̿ (︶︹︺)			He wasn't strong enough...
		}



		if (particleLifeTime < 0)
			KillParticle ();		//	(✖╭╮✖)        You killed him!		YOU ONLY EXECUTED THE CODE, RIGHT? THAT WASN'T YOUR FAULT?

	}




	void KillParticle(){
		Destroy (gameObject);
	}
}

































//(๑◕︵◕๑)  (▰︶︹︺▰)  ┏༼ ◉ ╭╮ ◉༽┓		You mourder...