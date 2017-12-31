using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingLogic : MonoBehaviour {

	public Vector3[] directions;
	public float directionChangeTime = 1f;
	public float moveSpeed;

	private int directionPointer;
	private float directionTimer;


	// Use this for initialization
	void Start () {
		directionPointer = 0;
		directionTimer = directionChangeTime;
	}
	
	// Update is called once per frame
	void Update () {
		directionTimer -= Time.deltaTime;
		if (directionTimer <= 0f) {
			directionTimer = directionChangeTime;
			directionPointer++;
			if (directionPointer >= directions.Length) {
				directionPointer = 0;
			}
		}

		//Make the Object Move
		GetComponent<Rigidbody> ().velocity = new Vector3 (
			directions[directionPointer].x * moveSpeed,
			GetComponent<Rigidbody>().velocity.y,
			directions[directionPointer].z * moveSpeed
		);
	}
}
