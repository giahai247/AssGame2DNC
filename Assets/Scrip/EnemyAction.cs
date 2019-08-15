using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {

	private Rigidbody2D enemybody;
	private bool checkStone;
	// Use this for initialization
	void Start () {
		//anh xa
		enemybody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float moveForce = 0f;
		if(checkStone){
			moveForce = -4f;
		}
		else{
			moveForce = 4f;
		}
		enemybody.velocity = new Vector2 (transform.localScale.x, 0) * moveForce;
	}
	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Stone") {
			checkStone = true;
		}
		if (target.gameObject.tag == "Stone2") {
			checkStone = false;
		}
	}
}
