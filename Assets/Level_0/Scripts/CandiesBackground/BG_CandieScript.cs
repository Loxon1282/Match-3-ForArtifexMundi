using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_CandieScript : MonoBehaviour {

	float xPos;
	float fallingSpeed,rotationSpeed;
	public float elementsVisibility = 0.4f;
	Color[] col = {Color.black,Color.cyan,Color.yellow,Color.red,Color.green,Color.white};

	SpriteRenderer sprite;
	int color;

	void Awake(){
		sprite = GetComponent<SpriteRenderer> ();
		color = Random.Range (0, 6); 
		sprite.color = new Color(col [color].r,col [color].g,col [color].b,col [color].a-elementsVisibility);		


		xPos = Random.Range (-3.0f, 3.1f);
		transform.position = new Vector2 (xPos, 5.5f);
		fallingSpeed = Random.Range (0.01f, 0.03f);
		rotationSpeed = Random.Range (2.0f, 8.0f);
	}


	void Update () {
		
		transform.position +=Vector3.down * fallingSpeed;
		transform.Rotate (0, 0, rotationSpeed);

		if (transform.position.y < -6)		
			Destroy (gameObject);
	}
}
