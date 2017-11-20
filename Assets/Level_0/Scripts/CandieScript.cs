using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandieScript : MonoBehaviour {

	bool touched = false;
	public int points,bonus = 20;
	public Vector2 startPos;
	Color[] col = {Color.black,Color.cyan,Color.yellow,Color.red,Color.green,Color.white};
	SpriteRenderer spr;
	public GameObject canCtr;
	CandiesController canCtrScript;
	public bool access = true;
	Quaternion startRot;

	void Awake(){
		
		spr = GetComponent<SpriteRenderer> ();
		spr.color = col [Random.Range (0, 6)];

		ColorsTag();

	}
	void Start () {
		points = 0;

		canCtr=GameObject.FindGameObjectWithTag("CandiesController");
		canCtrScript = canCtr.GetComponent<CandiesController> ();

		startPos = transform.position;
		startRot = transform.rotation;
		transform.position=new Vector2(startPos.x,startPos.y+40.0f);


	}

	void Update () {
		
	//===================================TOUCH CONTROL====================================================================================
		if (!canCtrScript.progress) {

			if (touched && Mathf.Abs (startPos.x - transform.position.x) < 0.3f) {
				transform.position = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, transform.position.y);
					
				if (startPos.x - transform.position.x < -0.3f) {
					//PRAWO
					canCtrScript.CandiesSwitch (startPos, true);
					touched = false;
				} else if (startPos.x - transform.position.x > 0.3f) {
					//LEWO				
					canCtrScript.CandiesSwitch (startPos, false);
					touched = false;
				}
			} else {
				touched = false;
			}
		}

//=============================================FALLING DOWN===========================================================================

		if (transform.position.x != startPos.x&&!touched||transform.position.y!=startPos.y) {
			transform.position = Vector2.Lerp (transform.position, startPos, 0.4f);
			transform.rotation = Quaternion.Lerp (transform.rotation, startRot,0.2f);
		}

	}
	void OnMouseDown(){
		touched = true;
	}
	void OnMouseUp(){
		touched = false;
	}
	void ColorsTag(){
		for (int i = 0; i < col.Length; i++) {
			if (spr.color == col [i])
				transform.tag = (i+1).ToString();
		}
	}
}
