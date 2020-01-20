using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	public float totalResistance = 0;
	public int totalClicks = 0;
	public Node[] allNodes;
	//public float meteorStrength = 0;

	// Use this for initialization
	void Start () {
		totalResistance = 0;
		totalClicks = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void HealAll (){
		foreach (Node node in allNodes) {
			node.HP += 5;
		}
	}

	/*
	public void newTurn(){
		totalResistance = 0;
		meteorStrength = Random.Range (100,300);
		//Select a random node to exhert force on
	}


	public void checkSurvival(){
		if (totalResistance >= meteorStrength) {
			newTurn ();
		} else {
			gameOver ();
		}
	}

	public void gameOver(){
		//Game Over Stuff
	}
	*/
}
