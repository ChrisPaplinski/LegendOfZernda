using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
	public GameObject model;
	public float timeToRotate = 2f;
	public float rotationSpeed = 6f;
	private int targetAngle;
	private float rotationTimer;
	private float shootingTimer;
	public GameObject bulletPrefab;
	public float reloadTimer = 1f;

	// Use this for initialization
	void Start () {
		rotationTimer = timeToRotate;
		shootingTimer = reloadTimer;
	}
	
	// Update is called once per frame
	void Update () {
		//Update enemy's angle.
		rotationTimer -= Time.deltaTime;
		if (rotationTimer <= 0) {
			rotationTimer = timeToRotate;

			targetAngle += 90;
		}
		//perform rotation.
		transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (0, targetAngle, 0), Time.deltaTime * rotationSpeed);

		//Shoot Bullets
		shootingTimer -= Time.deltaTime;
		if (shootingTimer <= 0f) {
			shootingTimer = reloadTimer;

			GameObject bulletObject = Instantiate (bulletPrefab);
			bulletObject.transform.position = transform.position + model.transform.forward;
			bulletObject.transform.forward = model.transform.forward;
		}
	}
}
