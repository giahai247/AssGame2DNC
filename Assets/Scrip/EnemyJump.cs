using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour {

	private Rigidbody2D enemybody;
	private bool checkBottom;
	// Use this for initialization
	void Start () {
		//anh xa
		enemybody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		float moveForceY = 0f;
		if(checkBottom){
			moveForceY = -6f;
		}
		else{
			moveForceY = 6f;
		}
		enemybody.velocity = new Vector2 (0,transform.localScale.y) * moveForceY;
	}
	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Botton") {
			checkBottom = false;
		}
		if (target.gameObject.tag == "Maxhightcret") {
			checkBottom = true;
		}
	}
}
