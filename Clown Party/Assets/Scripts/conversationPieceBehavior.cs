using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationPieceBehavior : MonoBehaviour {
	public Sprite[] possibleSprites;
	SpriteRenderer myRenderer;
	[HideInInspector]
	public int myValue;
	private playerBehavior _player;
	private handBehavior zaHando;
	void Awake () {
		//Select a random indexable integer between 0 and 5
		myValue = Random.Range(0,6);
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
		myRenderer.sprite = possibleSprites [myValue];
		_player = GameObject.FindObjectOfType<playerBehavior> ();
		zaHando = GameObject.FindObjectOfType<handBehavior> ();
		transform.LookAt(_player.transform);
	}

	//STILL NOT FULLY TESTED FOR CASE OF FULL HAND
	public void OnMouseDown(){
		//If the player has room in the inventory, add and delete
		if (!zaHando.isFull) {
			zaHando.takeCard(myValue);
			DestroyObject (gameObject);
		} else {
			Debug.Log ("Hand is Already Full!");
		}
	}
	
}
