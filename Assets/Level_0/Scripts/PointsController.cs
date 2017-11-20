using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour {

	public Text pointsText;
	int points;
	bool showBonus=true;
	public int bonus=20;
	public GameObject bonusBoard;
	Animator bonusBoardAnim;


	// Use this for initialization
	void Start () {
		bonusBoardAnim = bonusBoard.GetComponent<Animator> ();
		points = 0;
		bonusBoard.SetActive (false);
		bonusBoardAnim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		pointsText.text = "POINTS: " + points;
		Debug.Log ("Points: " + points);
	}

	void GetPoint(){
		points++;
	}
	void GetBonus(){
		points += bonus;
	}
	void GetDoubleBonus(){
		points += bonus*2;
	}
	void BonusBoardAnim(){
		if (showBonus) {
			StartCoroutine ("animWait");
			bonusBoard.SetActive (false);
			bonusBoard.SetActive (true);
			bonusBoardAnim.enabled = true;
			bonusBoardAnim.Play ("InfoTab");
			showBonus = false;
		}
	}
	IEnumerator animWait(){
		yield return new WaitForSeconds (4);
		showBonus = true;
	}
}
