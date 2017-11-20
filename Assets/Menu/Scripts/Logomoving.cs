using UnityEngine;
using System.Collections;

public class Logomoving : MonoBehaviour {
	public int speed = 15;
	public int high =16;
	float x;
	// Use this for initialization
	void Start () {
		x = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x, Mathf.PingPong (Time.time*15, 16)+x);

	}
}
