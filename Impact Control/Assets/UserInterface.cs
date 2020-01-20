using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UserInterface : MonoBehaviour {
	public Text clickCounter;
	public GameObject[] lossStuff;
	public Board _gameBoard;
	void Start(){
		foreach (GameObject item in lossStuff) {
			item.SetActive (false);
		}
		_gameBoard = GameObject.FindObjectOfType<Board> ();
	}

	public void loss(){
		Time.timeScale = 0;
		foreach (GameObject item in lossStuff) {
			item.SetActive (true);
		}
	}

	public void UpdateClickCounter (){
		clickCounter.text = "Total Clicks: " + _gameBoard.totalClicks;
	}

	public void restart(){
		SceneManager.LoadScene ("Main Scene");	
	}

	public void quit(){
		Application.Quit();
	}
}
	