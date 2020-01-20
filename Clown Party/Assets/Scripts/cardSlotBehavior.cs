using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardSlotBehavior : MonoBehaviour {
	public bool isOpen = true;
	public bool selected = false;
	public Sprite[] possibleImages;
	public int myValue;
	private playerBehavior _player;
	public Image myImage;
	private Button myButton;
	private Color originalPalor;
	public Color highlightPalor;
	//Note: the value of 6 is a blank face
	void Start () {
		//no card should have a value above 5
		myValue = 6;
		myImage = GetComponent<Image> ();
		myImage.sprite = possibleImages [6];//Initialized Blank
		myButton = gameObject.GetComponent<Button>();
		myButton.interactable = false;
		//Saves the original color for manual highlighting
		originalPalor = new Color (myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a);
		_player = GameObject.FindObjectOfType<playerBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void refreshFace (int faceValue){
		//If the amount of cards in the hand are at or greater than this id
		selected = false;
		myImage.color = originalPalor;
		myImage.sprite = possibleImages [faceValue];
		myValue = faceValue;
		//If the facevalue is 6, this is blank, and shouldn't be interactable
		myButton.interactable = (faceValue != 6);
		if (faceValue == 6) {
			selected = false;
		}

		//This will be called to be 6 when the card is used
	}


	public void toggleSelect(){
		//Nothing should happen if the card is blank
		if (myImage.sprite != possibleImages [6]) {
			//Switch the value of selected
			selected = !selected;
		}

		if (selected) {
			myImage.color = highlightPalor;
		} else {
			myImage.color = originalPalor;
		}
	}
}
	
