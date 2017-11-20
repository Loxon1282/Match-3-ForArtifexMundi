using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Generating: MonoBehaviour {

	float marginTop = 2.5f;
	public GameObject candy;
	Camera cam;
	public GameObject[,] candies = new GameObject[9,9];
	public Vector2[,] candiesPos = new Vector2[9,9];

	public GameObject CandiesControllerObj;

	CandiesController candieCtrl;

	void Start () {
		cam = Camera.main;
		candieCtrl = CandiesControllerObj.GetComponent<CandiesController> ();

		BoardGenerate (9, 9);

	}

	void BoardGenerate(int numberOfElementsX,int numberOfElementsY)
	{
		
		float spacingX = 2f * cam.orthographicSize*cam.aspect / numberOfElementsX;
		float spacingY = (2f * cam.orthographicSize-marginTop) / numberOfElementsY;


		for (int i = 0; i < numberOfElementsY; i++) {
			for (int j = 0; j < numberOfElementsX; j++) {
				

				candies [i, j] = Instantiate(candy,new Vector2(j*spacingX-(cam.orthographicSize*cam.aspect)+spacingX/2,(i*spacingY-(cam.orthographicSize))+spacingY/2 ),candy.transform.rotation); 

				candiesPos [i, j] = candies [i, j].transform.position;


			}
		}
		//Responsywnie rozmieszcza elementy na tablicy w każdej orientacji. Nie dostosowuję już UI i wielkości do każdej orientacji, bo nie jest to celem zadania	
	}
	public void BoardRefill(){
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {


				if (candies [i, j] == null) {
					candies [i, j] = Instantiate (candy, candiesPos [i, j],candy.transform.rotation);

				
				}


			}
		}
		if (!candieCtrl.noMatch) {
			
			candieCtrl.MatchColors();

		} 
		else
			candieCtrl.progress = false;
	}
	public void BoardReschuffle(){
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				Destroy(candies [i, j]);

			}
		}
		BoardGenerate (9, 9);
		candieCtrl.StartCoroutine ("WaitForBoardsReady");
	}

}
