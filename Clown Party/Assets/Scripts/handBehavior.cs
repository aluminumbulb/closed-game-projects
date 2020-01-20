using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class monitors information about each of the six card slots
public class handBehavior : MonoBehaviour {
	public List<int> cardsInHand = new List<int>();
	public cardSlotBehavior[] availableSlots;
	public bool isFull = false;
	private clownBehavior theClown;
	//[HideInInspector]
	public float totalScore;
	void Start () {
		theClown = GameObject.FindObjectOfType<clownBehavior> ();
		totalScore = 0;
	}

	void Update(){
		if (Input.GetKey (KeyCode.E)) {
			checkSelected();
		}
	}

	public void takeCard (int valueIn){
		cardsInHand.Add (valueIn);
		cardsInHand.Sort ();
		showHand ();
		isFull = (cardsInHand.Count>=5);
	}

	void showHand(){
		//in the following way if there are no cards this will be -1
		int indecesAvailable = cardsInHand.Count-1;
		//5 selecting instead of "cardsInHand.count"
		for(int i = 0; i<5; i++){
			if (i <= indecesAvailable) {
				availableSlots[i].refreshFace(cardsInHand[i]);
			}else{
				availableSlots [i].refreshFace (6);
			}
		}
	}


	public void checkSelected ()
	{ 
		//This represents number of occurences by value
		int[] possibleSets = { 0, 0, 0, 0, 0, 0 };
		//List<List<cardSlotBehavior>> cardSets = new List<List<cardSlotBehavior>> ();

		//for (int i = 0; i < 6; i++) {
		//	cardSets.Add (new List<cardSlotBehavior> ());
		//}

		/*the card's value will increment the slot designated for that value*/
		foreach (cardSlotBehavior card in availableSlots) {
			if (card.selected) {
				possibleSets [card.myValue] += 1;
			}
		}
		spendIfRun (possibleSets);
		spendIfTwoThreeOrFull (possibleSets);
		//Update look
		showHand ();
	}

	void spendIfRun(int[] sets){
		List<int> usedCards = new List<int> ();
		for (int i = 1; i < 5; i++) {
			if (sets[i] != 1) {
				//impossible, stop here
				return;
			} else {
				//add that value to the pile
				usedCards.Add(i);
			}
		}

		if ((sets[0] == 1) && (sets[5] == 1)) {
			return;
		}

		if ((sets[0] != 1) && (sets[5] != 1)) {
			return;
		}

		if (sets[0] == 1) {
			usedCards.Add (0);
		} else {
			usedCards.Add (5);
		}

		foreach(int card in usedCards){
			cardsInHand.Remove(card);
		}
		Debug.Log ("Run");
		updateScore (100);
		isFull = false;
	}


	void spendIfTwoThreeOrFull(int[] sets){
		bool threeOfAKind = false;
		bool twoOfAKind = false;

		for(int i =0; i<6; i++){
			if (sets[i] >= 3 && threeOfAKind == false) {
				threeOfAKind = true;
				sets [i] -= 3;
				cardsInHand.Remove (i);
				cardsInHand.Remove (i);
				cardsInHand.Remove (i);
				Debug.Log ("Three of A Kind");
				updateScore (30);
				isFull = false;
			}

			if (sets[i] >= 2 && twoOfAKind == false) {
				sets [i] -= 2;
				twoOfAKind = true;
				List<int> spendSet = new List<int> ();
				cardsInHand.Remove (i);
				cardsInHand.Remove (i);
				Debug.Log ("Two of a Kind");
				updateScore (20);
				isFull = false;
			}
		}

		if (threeOfAKind && twoOfAKind) {
			//Adds an additional bonus on top of the 50 that must already be in the bank
			Debug.Log("Full House");
			updateScore (20);
			isFull = false;
		}


	}

	void updateScore(float score){
		theClown.satiate (score);
		totalScore += score;
	}
}