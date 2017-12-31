using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int health = 10;
	[Header("Movement")]
	public GameObject model;
	public float moveSpeed = 5f;
	public float jumpSpeed = 5f;
	public float rotationSpeed = 2f;
	private bool canJump;
	public float knockbackForce;

	private float knockbackTimer = 1f;

	[Header ("Equpment")]
	public Sword sword;
	public GameObject bombPrefab;
	public int ammoBomb = 5;
	public float throwingSpeed;
	public Bow bow;
	public float quiverSize = 10f;
	private Rigidbody playerRigidbody;
	private Quaternion targetModelRotation;


	// Use this for initialization
	void Start () {
		bow.gameObject.SetActive (false);
		playerRigidbody = GetComponent<Rigidbody> ();
		targetModelRotation = Quaternion.Euler (0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		model.transform.rotation = Quaternion.Lerp (model.transform.rotation, targetModelRotation, Time.deltaTime * rotationSpeed);
		 if (knockbackTimer >0) {
			 knockbackTimer -= Time.deltaTime;
		 } else {
			ProcessInput ();
		 }
	}
	//Process all input
	public void ProcessInput() {
		//Right
		if (Input.GetKey ("right")) {
			playerRigidbody.velocity = new Vector3 (
				-moveSpeed,
				playerRigidbody.velocity.y,
				playerRigidbody.velocity.z
			);
			targetModelRotation = Quaternion.Euler (0, 270, 0);
		}
		//Left
		if (Input.GetKey ("left")) {
			playerRigidbody.velocity = new Vector3 (
				moveSpeed,
				playerRigidbody.velocity.y,
				playerRigidbody.velocity.z
				);
			targetModelRotation = Quaternion.Euler (0, 90, 0);
		}
		//Up
		if (Input.GetKey ("up")) {
			playerRigidbody.velocity = new Vector3 (
				playerRigidbody.velocity.x,
				playerRigidbody.velocity.y,
				-moveSpeed
			);
			targetModelRotation = Quaternion.Euler (0, 180, 0);
		}
		//Down
		if (Input.GetKey ("down")) {
			playerRigidbody.velocity = new Vector3 (
				playerRigidbody.velocity.x,
				playerRigidbody.velocity.y,
				moveSpeed
			);
			targetModelRotation = Quaternion.Euler (0, 0, 0);
		}

		//Raycast collision Jump check
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.01f)){
			canJump = true;
		}

		//Jump Logic
		if(Input.GetKeyDown("space") && canJump){
			canJump = false;
			playerRigidbody.AddForce(Vector3.up * jumpSpeed);
			playerRigidbody.velocity = new Vector3(
				playerRigidbody.velocity.x,
				jumpSpeed,
				playerRigidbody.velocity.z

			);
		}

		//Check equipment interaction 
		if (Input.GetKeyDown("z")) {
			sword.gameObject.SetActive (true);
			bow.gameObject.SetActive (false);
			sword.Attack();
		}
		if (Input.GetKeyDown("c")) {
			ThrowBomb ();
		}
		if (Input.GetKeyDown("x")) {
			if (quiverSize > 0) {
				sword.gameObject.SetActive (false);
				bow.gameObject.SetActive (true);
				bow.Attack ();
				quiverSize--;
			}
		}
	}	
	
	private void ThrowBomb () {

		if (ammoBomb <=0) {
			return;
		}

		GameObject bombObject = Instantiate (bombPrefab);
		bombObject.transform.position = transform.position + model.transform.forward + model.transform.up;

		Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;

		bombObject.GetComponent<Rigidbody>().AddForce (throwingDirection * throwingSpeed);

		ammoBomb--;
	}

	void OnTriggerEnter (Collider otherCollider) {
		if (otherCollider.GetComponent<EnemyBullet> () != null) {
			Hit ((transform.position - otherCollider.transform.position).normalized);
			Destroy (otherCollider.gameObject);
		}	
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.GetComponent<Enemy> () != null) {
			Hit ((transform.position - collision.transform.position).normalized);
		}
	}

	private void Hit (Vector3 direction) {
		Vector3 knockbackDirection = (direction + Vector3.up).normalized;
		playerRigidbody.AddForce (knockbackDirection * knockbackForce);
		knockbackTimer = 1f;

		health--;
		if (health <=0) {
			Destroy (gameObject);
		}
	}
}
