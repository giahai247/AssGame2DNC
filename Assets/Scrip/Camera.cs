using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player_0").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			Vector3 temp = transform.position;
			temp.x = player.position.x;
			transform.position = temp;
		}
	}
}
