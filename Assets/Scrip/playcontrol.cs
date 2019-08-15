using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Collections;
using System.Collections.Generic;

public class playcontrol : MonoBehaviour {
	//khai bao thuoc tinh Rigid,animator,status
	private Rigidbody2D mybody;
	private Animator anim;
	public Text textscore;
	private int score;
	//khai bao control
	public bool grounded;
	public bool checkdie;
	public bool checklevel;

	private List<GameObject> listObjs;
	private GameObject[] arrayObjs;
	int i=0;

	//khai bao hieu ung
	public ParticleSystem effect;
	// Use this for initialization
	void Start () {
		checkdie = false;
		listObjs = new List<GameObject> ();
		arrayObjs = GameObject.FindGameObjectsWithTag ("blood");
		foreach (GameObject obj in arrayObjs){
			listObjs.Add (obj);
		}
	}

	void Awake(){
		//anh xa doi tuong trong Hỉeachy
		mybody = GetComponent<Rigidbody2D>();
		//anh xa animator
		anim = GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		Control();
	}
	public void Control(){
		//khai bao gia tri ban dau
		float forceX= 0f;
		float forceY = 0f;
		//khai bao gia tri nhan van toc hien tai cua chẩcter
		float velo = Mathf.Abs(mybody.velocity.x);
		//nhan hanh dong tu ban phim
		float key = Input.GetAxisRaw("Horizontal");
		//kiem tra xem nguoi dung nhan vao phim mui ten trai hay phai
		if(key>0){
			if(velo<4f){
				forceX = 15f;

			}
			//thuc hien hanh dong di chuyen
			anim.SetBool("Play", true);
			//hanh dong xoay mat
			Vector2 scale = transform.localScale;
			scale.x = 1.5f;
			transform.localScale = scale;
		}
		else if(key < 0){
			//kiem tra van toc
			if(velo < 4){
				forceX = -15f;
			}
			//gan trang thai
			anim.SetBool("Play", true);
			//xoay mat
			Vector2 scale = transform.localScale;
			scale.x = -1.5f;
			transform.localScale = scale;

		}else{
			anim.SetBool("Play", false);
		}
		//jump
		if(Input.GetKey(KeyCode.Space)){
			// chia nhay khi cham tren dat
			if(grounded){
				forceY = 300;
				grounded = false;
				anim.SetBool ("Jump", true);
			}
			else{
				anim.SetBool ("Jump", false);
			}
		}
		//checkdie
		if(checkdie){
			anim.SetBool ("Playdie",true);
		}
		//cap nhat thong tin vi tri
		mybody.AddForce(new Vector2(forceX,forceY));
	}
	//ham kiemtra va cham
	void OnCollisionEnter2D(Collision2D target){
		if(target.gameObject.tag == "Botton"){
			grounded = true;
		}
		if(target.gameObject.tag == "Box"){
			grounded = true;
		}if(target.gameObject.tag == "Stone2"){
			grounded = true;
		}
	
		if (target.gameObject.tag == "Cret")
		{
			Destroy(listObjs[i]);
			listObjs.RemoveAt(i);
			if(listObjs.Count == 0)
			{
				SceneManager.LoadScene("Gameover");
			}
			Debug.Log("Destroy object: " + i.ToString());


		}
		if(target.gameObject.tag == "Home"){
			checkdie = true;
			SceneManager.LoadScene ("Gaming");
		}
		if(target.gameObject.tag == "win"){
			checkdie = true;
			SceneManager.LoadScene ("win");
		}
		if (target.gameObject.tag == "Coin")
		{
			score++;
			Destroy(target.gameObject);
			Destroy(Instantiate(effect, this.transform.position, this.transform.rotation) as ParticleSystem, 2);
			textscore.text = "Score : " + score.ToString();
		}

	}
	//kiem tra va cham kieu trigger
	void OnTriggerEnter2D(Collider2D other){
		//neu va cham voi coin an diem
		//if(other.gameObject.CompareTag("Coin")){
		//	Animator otheranim = other.gameObject.GetComponent<Animator>() as Animator;
		//	otheranim.SetBool ("CoinHide", true);
		//	score = score + 1;
		//	textscore.text = "Score: " + score.ToString ();
		//	Destroy(other.gameObject); 
		//
		//	Destroy(Instantiate(effect, this.transform.position, this.transform.rotation) as ParticleSystem, 2);



		//}
		if (other.gameObject.tag == "Coin")
		{
			score++;
			Destroy(other.gameObject);
			Destroy(Instantiate(effect, this.transform.position, this.transform.rotation) as ParticleSystem, 2);
			textscore.text = "Score : " + score.ToString();
		}
		if(other.gameObject.CompareTag("Box")){
			Animator otheranim = other.gameObject.GetComponent<Animator>() as Animator;
			otheranim.SetBool ("Box", true);
			Destroy (other.gameObject,1);
		}

	}

	//viet ham GoCT
	public void GoCT(){
		float forceX = 0f;
		float forceY = 0f;

		float velo = Mathf.Abs (mybody.velocity.x);
		if(velo < 4f){
			forceX = 155f;
		}
		//set hanh dong di chuyen
		anim.SetBool("walked",true);
		//xoay mat
		Vector2 scale = transform.localScale;
		scale.x = 1.5f;
		transform.localScale = scale;
		if(checkdie){
			anim.SetBool ("playdie",true);
		}
		//cap nhat vi tri moi
		mybody.AddForce (new Vector2 (forceX, forceY));
	}
	public void BackCT(){
		float forceX = 0f;
		float forceY = 0f;

		float velo = Mathf.Abs (mybody.velocity.x);
		if(velo < 4f){
			forceX = -155f;
		}
		//set hanh dong di chuyen
		anim.SetBool("walked",true);
		//xoay mat
		Vector2 scale = transform.localScale;
		scale.x = -1.5f;
		transform.localScale = scale;
		if(checkdie){
			anim.SetBool ("playdie",true);
		}
		//cap nhat vi tri moi
		mybody.AddForce (new Vector2 (forceX, forceY));
	}

	public void JumpCT(){
		float forceX = 0f;
		float forceY = 0f;

		float velo = Mathf.Abs (mybody.velocity.x);
		float key = Input.GetAxisRaw("Horizontal");
		if(key >0){
			if(velo < 4f){
				forceX = 15f;
			}
			//set hanh dong di chuyen
			anim.SetBool("walked",true);
			//xoay mat
			Vector2 scale = transform.localScale;
			scale.x = 1.5f;
			transform.localScale = scale;
		}
		else if(key < 0){
			if(velo < 4){
				forceX = -15f;
			}
			//set hanh dong di chuyen
			anim.SetBool("walked",true);
			//xoay mat
			Vector2 scale = transform.localScale;
			scale.x = -1.5f;
			transform.localScale = scale;
		}
		else{
			anim.SetBool("walked",false);
		}

		if(grounded){
			forceY = 300;
			grounded = false;
			anim.SetBool ("Jump", true);
		}
		else{
			anim.SetBool ("Jump", false);
		}
		if(checkdie){
			anim.SetBool ("playdie",true);
		}
		//cap nhat vi tri moi
		mybody.AddForce (new Vector2 (forceX, forceY));
	}



}

