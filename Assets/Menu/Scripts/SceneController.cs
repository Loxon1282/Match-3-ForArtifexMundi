using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public GameObject board;
	bool boardDown=false;
	Animator boardAnim;

	void Start () {
		if(SceneManager.GetActiveScene().buildIndex==0)
		boardAnim = board.GetComponent<Animator> ();
		
	}

	public void ShowBoard(){
		if (!boardDown) {
			boardAnim.Play ("InfoBoardAnim 1");
			boardDown = true;
		} else {
				boardAnim.Play ("InfoBoardAnim 0");
			boardDown = false;
		}
	}
	public void LoadLevel(int index){
		SceneManager.LoadScene (index);
	}
	public void Exit(){
		Application.Quit ();
	}
}
