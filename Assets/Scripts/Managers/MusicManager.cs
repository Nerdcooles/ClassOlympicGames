using UnityEngine;
using System.Collections;

public class MusicManager :  Singleton<MusicManager> { 
	
	private AudioSource source;
	
	private AudioClip menuClip;

	public static string[] songs = {"Audio/ANightOfDizzySpells", "Audio/HerosDayOff"};

	void Awake() { 
		DontDestroyOnLoad(gameObject);  
		source = gameObject.AddComponent<AudioSource>(); 
		source.loop = true;

		menuClip = Resources.Load <AudioClip> ("Audio/PinkFloyd_AnotherBrickinTheWall");
	}
		
	void Start() { 
		source.clip = menuClip;
		source.Play ();
	}

	public AudioSource Source {
		get {
			return source;
		}
		set {
			source = value;
		}
	}

	public AudioClip MenuClip {
		get {
			return menuClip;
		}
	}
}
