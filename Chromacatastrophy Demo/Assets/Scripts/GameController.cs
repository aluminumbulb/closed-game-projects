using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameController : MonoBehaviour {
	public static GameController controller;

	[HideInInspector]
	public Vector3 lastSavedLocation;

	[HideInInspector]
	public String lastRoomSaved;

	public AudioSource audioSource;
	public AudioClip bg1, bg2, bg3, bg4;

	public bool orbGetRed = false;
	public bool orbGetGreen = false;
	public bool orbGetBlue = false;


	private Orb_Get orbInRoom;
	private ColorChangeProps[] ccp;
	private GreenRoomTileSwap[] gts;
	private TilemapSwitchable[] tms;

	public Player _player;

	void Awake(){
		lastSavedLocation = new Vector3 (0, -2, 0);
	//This is a pho-singleton pattern which allows for only one game controller to exist		
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;

		} else if (controller != null) {
			Destroy (this);
		}

		//Alter this code if ever more than one orb is in a room
		orbInRoom = GameObject.FindObjectOfType<Orb_Get>();
			
	}

	void Start(){
		adjustPlayerAbilities ();
		paintWorld ();

		audioSource = GetComponent<AudioSource> ();
	}

	void adjustPlayerAbilities(){
		_player = FindObjectOfType<Player> ();
		if (_player == null) {
			Debug.Log ("player not found");
		}
		_player.canDoubleJump = orbGetRed;	
		_player.canWallSlide = orbGetGreen;
		_player.canDash = orbGetBlue;
	}

	public void paintWorld(){
		ccp = UnityEngine.Object.FindObjectsOfType<ColorChangeProps> ();
		tms = UnityEngine.Object.FindObjectsOfType<TilemapSwitchable> ();
		gts = UnityEngine.Object.FindObjectsOfType<GreenRoomTileSwap> ();
		adjustPlayerAbilities ();

		foreach (ColorChangeProps prop in ccp) {
			prop.colorize ();
			if (prop.isObstructive) {
				if (orbGetRed && prop.colorArea == ColorChangeProps.ColorArea.Red) {
					prop.solidify ();

				} else if (orbGetGreen && prop.colorArea == ColorChangeProps.ColorArea.Green) {
					prop.solidify ();

				} else if (orbGetBlue && prop.colorArea == ColorChangeProps.ColorArea.Blue) {
					prop.solidify ();

				} else {
					prop.liquify ();
				}
			}
		}
			

		foreach (TilemapSwitchable tilemap in tms) {
			tilemap.colorize ();
		}

		foreach (GreenRoomTileSwap tilemap in gts) {
			tilemap.colorize ();
		}
	}
	// Update is called once per frame
	void Update () {
		if (_player == null) {
			_player = FindObjectOfType<Player> ();
		}


		manageMusic ();
	}


	void manageMusic(){
		if (!audioSource.isPlaying) {
			audioSource.loop = true;
			if (!orbGetRed)
				audioSource.volume = .80f;
			audioSource.clip = bg1;
			audioSource.Play ();

			if (orbGetRed && !(orbGetGreen) && !(orbGetBlue))
				audioSource.clip = bg2;
			audioSource.volume = .82f;
			audioSource.Play ();

			if (orbGetGreen && !(orbGetBlue))
				audioSource.clip = bg3;
			audioSource.volume = .85f;
			audioSource.Play ();

			if (orbGetBlue)
				audioSource.clip = bg4;
			audioSource.volume = .86f;
			audioSource.Play ();
		}
	}
		
	public void Save(Vector3 lastSavedLocation, String lastRoomSaved){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
	
		GameData data = new GameData ();
		data.orbGetRed = orbGetRed;
		data.orbGetGreen = orbGetGreen;
		data.orbGetBlue = orbGetBlue;
		data.playerX = lastSavedLocation.x;
		data.playerY = lastSavedLocation.y;
		data.lastRoomSaved = lastRoomSaved;
		bf.Serialize (file, data);
		file.Close ();

		//Debug.Log (lastSavedLocation.ToString ());
		Debug.Log ("Saved");
	}

	public void Load (){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			GameData data = (GameData)bf.Deserialize (file);
			orbGetRed = data.orbGetRed;
			orbGetGreen = data.orbGetGreen;
			orbGetBlue = data.orbGetBlue;
			lastSavedLocation = new Vector3 (data.playerX, data.playerY,0f);
			lastRoomSaved = data.lastRoomSaved;

			if(lastRoomSaved != SceneManager.GetActiveScene().name){
				SceneManager.LoadScene (lastRoomSaved, LoadSceneMode.Single);
			}
			_player = FindObjectOfType<Player> ();
			_player.transform.position = lastSavedLocation;
			if (orbInRoom.myOrbType == Orb_Get.orbType.Red) {
				if (orbGetRed) {
					orbInRoom.gotten ();
				}
			}
			if (orbInRoom.myOrbType == Orb_Get.orbType.Green) {
				if (orbGetGreen) {
					orbInRoom.gotten ();
				}
			}
			if (orbInRoom.myOrbType == Orb_Get.orbType.Blue) {
				if (orbGetBlue) {
					orbInRoom.gotten ();
				}
			}
			paintWorld ();
		}

		Debug.Log ("Loaded");
	}
		
}

[Serializable]
class GameData{
	public bool orbGetRed;
	public bool orbGetGreen;
	public bool orbGetBlue;
	public float playerX;
	public float playerY;
	public String lastRoomSaved;
}
