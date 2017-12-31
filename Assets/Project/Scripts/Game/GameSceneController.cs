using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour {
	[Header("Game")]
	public PlayerController player;

	[Header ("UI")]
	public GameObject[] hearts;
	public Text bombText;
	public Text arrowText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			for (int i=0; i<hearts.Length; i++){
				hearts [i].SetActive (i<player.health);
			}
			bombText.text = "Bombs: " + player.ammoBomb;
			arrowText.text = "Arrows: " + player.quiverSize;
		} else {
			for (int i=0; i<hearts.Length; i++){
				hearts [i].SetActive (false);
			}
		}
	}
}
