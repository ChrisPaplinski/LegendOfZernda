using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public float arrowSpeed;
	public float lifetime = 1f;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			Destroy (gameObject);
		}
	}
}
