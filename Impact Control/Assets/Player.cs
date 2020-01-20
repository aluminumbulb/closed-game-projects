using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public UserInterface ui;
	public int currentStrain;
	// Use this for initialization
	void Start () {
		ui = GameObject.FindObjectOfType<UserInterface> ();
		currentStrain = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReceiveImpact(int impactValue){
		currentStrain = impactValue;
		//Player Selects Nodes from Menu and set the amount for each to take
		//Current strain changes as nodes are updated
		//Player Presses Turn
	}

	public void PassImpact(){
		//for every adjacent node, pass along the value involved
	}
}
