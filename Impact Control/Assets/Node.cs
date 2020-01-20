using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	public float HP = 100f;
	public float reportedResistance = 0f;
	public Node[] neighbors;
	public Board gameBoard;
	private Renderer _myRenderer;
	public UserInterface ui;

	void Start(){
		gameBoard = GameObject.FindObjectOfType<Board> ();
		_myRenderer = gameObject.GetComponent<Renderer> ();
		ui = GameObject.FindObjectOfType<UserInterface> ();
	}

	public void ReceiveImpact(float impactValue){
		reportedResistance = 0;
		float remainingImpact = impactValue;
		float sacrificeAmount = (HP / 6);
		//All nodes receiving impact have a little bit of initial damage
		reportedResistance = 5;
		remainingImpact -= 5;
		HP -= 5;

		//Pass along impact
		if (remainingImpact >= sacrificeAmount) {
			reportedResistance += sacrificeAmount;
			gameBoard.totalResistance += reportedResistance;

			List<Node> elegableNeighbors = new List<Node> ();
			//Check which neighbors can be sent impact
			foreach (Node neighbor in neighbors) {
				//15 chosen as an arbitrary value to make sure a neighbor can handle strain
				if (neighbor.HP > 15) {
					elegableNeighbors.Add (neighbor);
				}
			}

			foreach (Node neighbor in elegableNeighbors) {
				//send each neihbor an average slice
				neighbor.ReceiveImpact ((impactValue - reportedResistance) / elegableNeighbors.Count);
				Debug.Log ((impactValue - reportedResistance) / elegableNeighbors.Count);
			}
		} else {
			reportedResistance += remainingImpact;

		}
		//Update the game board as to what's been going on

		remainingImpact = 0;
		HP -= reportedResistance;
		gameBoard.totalResistance += reportedResistance;


		if (HP > 70) {
			_myRenderer.material.color = Color.green;
		} else if (HP <= 70 && HP >= 30) {
			_myRenderer.material.color = Color.yellow;
		} else if (HP < 30 && HP >0) {
			_myRenderer.material.color = Color.red;
		} else {
			Debug.Log ("Someone Is Dead");
			Destroy (gameObject);
			ui.loss ();
		}
	}

	void OnMouseDown(){
		ReceiveImpact (50);
		gameBoard.totalClicks += 1;
		ui.UpdateClickCounter ();
		gameBoard.HealAll ();
	}

}
