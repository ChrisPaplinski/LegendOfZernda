using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	public Teleporter exitTeleporter;
	public float exitOffset = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider otherCollider) {
		if (otherCollider.GetComponent<PlayerController> () != null) {
			if (exitTeleporter != null) {
				PlayerController player = otherCollider.GetComponent<PlayerController> ();
				player.transform.position = exitTeleporter.transform.position + exitTeleporter.transform.forward * exitOffset;
			}
		}
	}
}
