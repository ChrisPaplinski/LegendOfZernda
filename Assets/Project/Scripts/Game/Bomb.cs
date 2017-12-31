using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public float fuse = 5f;
	public float explosionRadius = 3f;
	public float explosionDuration = 0.25f;
	public GameObject explosionModel;
	private float fuseTimer;
	private bool exploded;
	// Use this for initialization
	void Start () {
		fuseTimer = fuse;
		explosionModel.transform.localScale = Vector3.one * explosionRadius;
		explosionModel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		fuseTimer -= Time.deltaTime;
		if (fuseTimer <= 0f && exploded == false){
			exploded = true;
			Collider [] hitObjects = Physics.OverlapSphere (transform.position, explosionRadius);
			foreach(Collider collider in hitObjects){
				Debug.Log (collider.name + " was hit!");
				if (collider.GetComponent<Enemy> () != null){
					collider.GetComponent<Enemy> ().Hit ();
				}
			}
			StartCoroutine (Explode());
		}
	}
	private IEnumerator Explode () {
		explosionModel.SetActive (true);
		yield return new WaitForSeconds (explosionDuration);
		Destroy (gameObject);
	}
}
