using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour {
	public bool hasSpace = true;
	//public List<int> cards = new List<int>();
	public cardSlotBehavior[] cardSlots;

	// Use this for initialization
	void Start () {
		//In the case of reloading a game
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
