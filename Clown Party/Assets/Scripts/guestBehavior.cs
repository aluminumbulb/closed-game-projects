 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guestBehavior : MonoBehaviour {
	public float attention = 100;
	public conversationPieceBehavior _conversationPiece;
	private conversationPieceBehavior myConversationPiece;
	private int refreshCounter = 100;
	public int conversationPieceExistance = 100;
	// Use this for initialization
	void Start () {
		refreshCounter = Random.Range (100, 300);
	}
	
	// Update is called once per frame
	void Update () {
		//The "Life" of this Party Guest
		if(attention<=0){
			GameObject.Destroy (gameObject);
		}

		//Counts down till peice goes away, takes damage if so
		if (myConversationPiece != null) {
			conversationPieceExistance -= 1;
			if (conversationPieceExistance < 0) {
				DestroyObject (myConversationPiece.gameObject);
				attention -= 25;
			}
		}
		else if (myConversationPiece == null && refreshCounter > 0) {
			refreshCounter -= 1;
		} 
		else if (myConversationPiece == null && refreshCounter <= 0) {
			myConversationPiece = Instantiate (_conversationPiece, (new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z)), Quaternion.identity);
			//reset conversation piece time
			conversationPieceExistance = 100;
			//Set refresh counter for next time
			refreshCounter = Random.Range (100, 300);
		}

	}
}
