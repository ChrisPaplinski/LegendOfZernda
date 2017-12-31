using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	public float bulletSpeed;
	public float lifetime = 1f;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			Destroy (gameObject);
		}
	}
}
