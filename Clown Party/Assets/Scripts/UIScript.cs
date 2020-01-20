using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
	// Use this for initialization
	public GameObject[] playScreen;
	public GameObject[] endScreen;
	public Text endText;
	private handBehavior _hand;

	void Start () {
		foreach (GameObject element in endScreen) {
			element.gameObject.SetActive (false);
		}

		_hand = GameObject.FindObjectOfType<handBehavior> ();
	}

	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			endGame ();
		}
	}

	public void changeToEndScreen(){
		foreach (GameObject element in playScreen) {
			element.gameObject.SetActive (false);
		}

		foreach (GameObject element in endScreen) {
			element.gameObject.SetActive (true);
		}

		endText.text = ("You satiated the clown " + _hand.totalScore + " humor points.");
	}

	public void restartSession(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void endGame(){
		Application.Quit ();
	}
}
