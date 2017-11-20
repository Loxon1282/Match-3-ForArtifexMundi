using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiesController : MonoBehaviour {

	public GameObject boardObject;
	Board_Generating bs;

	public GameObject pointsController;
	PointsController pc;

	GameObject[,] candies;
	Vector2[,] candiesPos;

	GameObject[] prompt = new GameObject[3];
	GameObject[] prompt2 = new GameObject[4];

	CandieScript candieScr;

	public GameObject wrongSound, correctSound;
	public GameObject particle;

	public bool noMatch,progress=false;
	int ileX,ileY;
	int[,] toDestroyS = new int[2, 2];		// Zapisuje elementy: [0,x] - do usunięcia na X(x,y) || [1,x] - do usunięcia na Y(x,y)

	public float timeToPrompt;	

	void Start () {
		bs = boardObject.GetComponent<Board_Generating> ();
		pc = pointsController.GetComponent<PointsController> ();


		timeToPrompt = 3.0f;
		candies =bs.candies;
		candiesPos = bs.candiesPos;
		StartCoroutine ("WaitForBoardsReady");
	}

	void Update () {

		if (!progress) {
			timeToPrompt -= Time.deltaTime;
		} else {
			timeToPrompt = 3.0f;
		}
		if (timeToPrompt < 0) {
			if (DeadEnd ()) {
				Debug.Log ("DEAD END");
				bs.BoardReschuffle ();
			}
			timeToPrompt = 3.0f;
		}

	}
//============================================= CHECKING BOARD ====================================================================
	public void MatchColors(){
		 noMatch = true;
		progress = true;

		// ZMIENIĆ NA ROZMIAR POLA GRY

		for (int i = 0; i <= 8; i++) {
			ileX = 0;
			ileY = 0;

			for (int j = 0; j < 8; j++) 
			{
				//--------------------------------------------SPRAWDZANIE OSI X--------------------------

				if (candies [i, j].tag == candies [i, j + 1].tag) {
					ileX++;
					if (ileX == 1) {
						toDestroyS [0, 0] = i;
						toDestroyS [0, 1] = j;

					}
					if (j == 7)
						ileX++;
				}
				else {
					if (ileX >= 2) {
						//-----------------------USUWANIE X--------------------------
						for (int k = 0; k < ileX + 1; k++) {
							
							Destroy(candies[toDestroyS[0,0],toDestroyS[0,1]+k]);
							Instantiate (particle, candies[toDestroyS[0,0],toDestroyS[0,1]+k].transform.position, particle.transform.rotation);
							pc.Invoke ("GetPoint",1);
							correctSound.SetActive (false);
							correctSound.SetActive (true);

						}
				//-----------------------POINTS--------------------------
						if (ileX > 2 && ileX < 4) {

							pc.Invoke ("GetBonus", 1);
						}
						else if (ileX > 3) {
							pc.Invoke ("GetDoubleBonus", 1);	
							pc.Invoke ("BonusBoardAnim", 1);		//Bonus animation 
						}
						noMatch = false;
					}
					ileX = 0;
				}
				//--------------------------------------------SPRAWDZANIE OSI Y--------------------------

				if (candies [j, i].tag == candies [j + 1, i].tag) {
					ileY++;
					if (ileY == 1) {
						toDestroyS [1, 0] = j;
						toDestroyS [1, 1] = i;
					}
					if (j == 7)
						ileY++;
				}
				else {
					if (ileY >= 2) {
						//-----------------------USUWANIE Y--------------------------
						for (int k = 0; k < ileY + 1; k++) {
							Destroy(candies[toDestroyS[1,0]+k,toDestroyS[1,1]]);
							Instantiate (particle, candies [toDestroyS [1, 0] + k, toDestroyS [1, 1]].transform.position, particle.transform.rotation);
							pc.Invoke ("GetPoint",1);
							correctSound.SetActive (false);
							correctSound.SetActive (true);
						}
						noMatch = false;
					//-----------------------POINTS--------------------------

						if (ileY > 2 && ileY < 4) {
							pc.Invoke ("GetBonus", 1);
						}
						else if (ileY > 3) {
							pc.Invoke ("GetDoubleBonus", 1);
							pc.Invoke ("BonusBoardAnim", 1);	//Bonus animation
						}
					}
					ileY = 0;
				}
			}

			//-----------------------USUWANIE X--------------------------
			if (ileX >= 3) {
				for (int k = 0; k < ileX; k++) {
					
					Destroy(candies[toDestroyS[0,0],toDestroyS[0,1]+k]);
					Instantiate (particle,candies[toDestroyS[0,0],toDestroyS[0,1]+k].transform.position, particle.transform.rotation);
					pc.Invoke ("GetPoint",1);
					correctSound.SetActive (false);
					correctSound.SetActive (true);
				}
				//-----------------------POINTS--------------------------
				if (ileX > 2 && ileX < 4) {
					pc.Invoke ("GetBonus", 1);
				}
					else if (ileX > 3) {
						pc.Invoke ("GetDoubleBonus", 1);
						pc.Invoke ("BonusBoardAnim", 1);		//Bonus animation
					}
				noMatch = false;

			}
			//-----------------------USUWANIE Y--------------------------
			if (ileY >= 3) {
				for (int k = 0; k < ileY; k++) {
					
					Destroy(candies[toDestroyS[1,0]+k,toDestroyS[1,1]]);
					Instantiate (particle,candies[toDestroyS[1,0]+k,toDestroyS[1,1]].transform.position, particle.transform.rotation);
					pc.Invoke ("GetPoint",1);
					correctSound.SetActive (false);
					correctSound.SetActive (true);

				}
				noMatch = false;
			//-----------------------POINTS--------------------------
						if (ileY > 2 && ileY < 4) {
							pc.Invoke ("GetBonus", 1);
						}
						else if (ileY > 3) {
							pc.Invoke ("GetDoubleBonus", 1);
							pc.Invoke ("BonusBoardAnim", 1);		//Bonus animation
						}
			}
		}
		if (noMatch == false)
			Invoke ("SlideDownCandy", 1);
		else
			progress = false;

	}
//===================================================CHECHING IF MOVE IS POSSIBLE=======================================================================
	bool DeadEnd(){
		int[] tags = new int[6];
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 8; j++) {

				for (int k = 0; k < 6; k++) {
					tags [k] = 0;
					for (int l = 0; l < 3; l++) {
						
						if (int.Parse(candies [i+l,j].tag)==k+1|| int.Parse(candies [i+l,j + 1].tag)==k+1) {
							tags [k]++;
						}

					}
					if (tags [k] > 2) {
						for (int l = 0; l < 3; l++) {

							if (int.Parse (candies [i + l, j].tag) == k + 1) {
								prompt [l] = candies [i + l, j];
							} else {
								prompt [l] = candies [i + l, j+1];
							}

						}

					}

				}	


				foreach (int tag in tags) {
					if (tag > 2) {
						for (int l = 0; l < 3; l++) {
							prompt [l].transform.Rotate (0, 0, 40);
						}
					//	Debug.Log (tag);
						return false;
					}
				}



			}
		}
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 6; j++) {
				for (int k = 0; k < 6; k++) {
					tags [k] = 0;
					for(int l=0;l<4;l++){
						if (int.Parse (candies [i, j + l].tag) == k+1)
							tags [k]++;
						}
					if(tags[k]>2){
						for (int l = 0; l < 4; l++) {
							if (int.Parse (candies [i, j + l].tag) == k+1)
								prompt2 [l] = candies [i, j + l];
						}
					}
				}
				foreach (int tag in tags) {
					if (tag > 2) {
						
							for (int l = 0; l < 4; l++) {
							if(prompt[0]==null&&prompt2[l]!=null)
								prompt2 [l].transform.Rotate (0, 0, 30);		//Podpowiada graczowi
							}

						return false;
					}
				}
			}
		}
		return true;
	}



//=========================================== SLIDING DOWN CANDIES =====================================================================

	void SlideDownCandy(){
		for (int i = 0; i < 9; i++) {
			
			for (int j = 0; j <9; j++) 
			{


				if (candies [i, j] == null) {
					
					for (int k = 1; k < 9 - i; k++) {
						if (candies [i + k, j] != null) {
							candies [i , j] = candies [i + k, j];
							//candies [i + k, j] = null;
							candieScr = candies [i, j].GetComponent<CandieScript> ();
							candieScr.startPos = candiesPos [i, j];
							candies [i + k, j] = null;
							k=9;

						}

					}
				}


					
			}
		}
		bs.BoardRefill ();
	}
//==========================================SWITCHING CANDIES==================================================================

	public void CandiesSwitch(Vector2 pos,bool prawo){
		progress = true;
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {

				if (pos == candiesPos [i, j]) {
					if (prawo&&j!=8) {
						
						GameObject tmpCandie;
						tmpCandie = candies [i, j + 1];

						candies [i, j + 1] = candies [i, j];
						candieScr = candies [i, j+1].GetComponent<CandieScript> ();
						candieScr.startPos = candiesPos [i, j + 1];

						candies [i, j] = tmpCandie;
						candieScr = candies [i, j].GetComponent<CandieScript> ();
						candieScr.startPos = candiesPos [i, j];
						MatchColors ();


						if (noMatch == true) {	
							wrongSound.SetActive (false);		//Informacja dźwiękowa
							wrongSound.SetActive (true);
							//Debug.Log (noMatch);
							tmpCandie = candies [i, j + 1];

							candies [i, j + 1] = candies [i, j];
							candieScr = candies [i, j+1].GetComponent<CandieScript> ();
							candieScr.startPos = candiesPos [i, j + 1];

							candies [i, j] = tmpCandie;
							candieScr = candies [i, j].GetComponent<CandieScript> ();
							candieScr.startPos = candiesPos [i, j];
						}
					}
					if (!prawo && j != 0) {
						GameObject tmpCandie;
						tmpCandie = candies [i, j - 1];

						candies [i, j - 1] = candies [i, j];
						candieScr = candies [i, j-1].GetComponent<CandieScript> ();
						candieScr.startPos = candiesPos [i, j -1];

						candies [i, j] = tmpCandie;
						candieScr = candies [i, j].GetComponent<CandieScript> ();
						candieScr.startPos = candiesPos [i, j];
						MatchColors ();


						if (noMatch == true) {
							
							wrongSound.SetActive (false);		//Informacja dźwiękowa
							wrongSound.SetActive (true);
							tmpCandie = candies [i, j - 1];

							candies [i, j - 1] = candies [i, j];
							candieScr = candies [i, j-1].GetComponent<CandieScript> ();
							candieScr.startPos = candiesPos [i, j -1];

							candies [i, j] = tmpCandie;
							candieScr = candies [i, j].GetComponent<CandieScript> ();
							candieScr.startPos = candiesPos [i, j];
						}

					
					}

				}


			}
		}

		MatchColors();

	}

	IEnumerator WaitForBoardsReady(){
		yield return new WaitForSeconds (0.2f);
		Invoke ("MatchColors", 1);
	}
		

}
