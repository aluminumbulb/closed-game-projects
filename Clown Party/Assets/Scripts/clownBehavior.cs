using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clownBehavior : MonoBehaviour {
	public float moveSpeed = 2f;
	public float humorDepricator = 1;
	public float depricationFactor = 1.1f;
	public float honkTime = 1000;
	private playerBehavior _player;
	private AudioSource _sound;
	public AudioClip honking;
	public UIScript _overlay;
	// Use this for initialization
	void Start () {
		_player = GameObject.FindObjectOfType<playerBehavior> ();
		transform.transform.LookAt(_player.transform);
		_sound = GetComponent<AudioSource> ();
		_sound.clip = honking;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (transform.position + (transform.forward * moveSpeed * Time.deltaTime));
		honkTime--;
		if (honkTime < 0) {
			honkTime = 300;
			_sound.Play ();
		}
	}

	public void satiate(float comedyValue){
		transform.position = ((-transform.forward * humorDepricator * comedyValue) + transform.position);
		humorDepricator = humorDepricator / depricationFactor;
	}

	private void OnTriggerEnter(Collider other){
			Debug.Log ("Trigger Entered");
			Time.timeScale = 0;
			_overlay.changeToEndScreen ();
	}
}
