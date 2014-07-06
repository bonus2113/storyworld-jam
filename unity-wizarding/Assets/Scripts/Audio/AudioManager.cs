using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public static float BaseMusicVolume = 0.2f;
	public GameObject PlayOnceEffectPrefab;
	public AudioClip[] SoundEffects;
	public AudioClip[] Music;
	public static bool SoundEnabled = true;
	public static bool MusicEnabled { get { return currentMusicSource.volume != 0; } set { currentMusicSource.volume = value ? BaseMusicVolume : 0; } }
	
	Dictionary<string, AudioClip> soundEffectLib = new Dictionary<string, AudioClip>();
	Dictionary<string, AudioClip> musicLib = new Dictionary<string, AudioClip>();
	static AudioManager Instance;
	static AudioSource currentMusicSource;
	[HideInInspector]
	public AudioSource MusicSource;
	[HideInInspector]
	public AudioSource CrossFadeSource;
	List<GameObject> activeSoundEffects = new List<GameObject>();

	static bool wasInitialized = false;

	// Use this for initialization
	void Awake () {
		if(!wasInitialized) {
			wasInitialized = true;
			Instance = this;
			soundEffectLib.Clear ();
			foreach(AudioClip clip in SoundEffects)
				soundEffectLib[clip.name] = clip;

			musicLib.Clear ();
			foreach(AudioClip clip in Music)
				musicLib[clip.name] = clip;

			AudioSource[] sources = GetComponents<AudioSource> ();
			MusicSource = sources [0];
			CrossFadeSource = sources [1];
			currentMusicSource = MusicSource;
		}
	}


	// Update is called once per frame
	void Update () {
		if(Instance != this)
			Instance = this;
		for(int i = activeSoundEffects.Count - 1; i >= 0; i--) {
			if(!activeSoundEffects[i])
				activeSoundEffects.RemoveAt(i);
		}
	}

	public static void PlayMusic( string _name, float _volume = 0.2f) {
		if (currentMusicSource.clip == Instance.musicLib [_name]) {
			BaseMusicVolume = _volume;
			currentMusicSource.volume = _volume;
			return;
		}
		Instance.CrossFadeSource.volume = 0;
		Instance.CrossFadeSource.clip = Instance.musicLib [_name];
		Instance.StartCoroutine (Instance.CrossfadeMusic(_volume));
	}

	IEnumerator CrossfadeMusic(float _newVolume = 0.2f) {
		Instance.CrossFadeSource.Play ();

		if(MusicEnabled) {
			float sourceVol = Instance.MusicSource.volume;
			while (Instance.MusicSource.volume > 0) {
				Instance.CrossFadeSource.volume += _newVolume * Time.deltaTime;
				Instance.MusicSource.volume -= sourceVol * Time.deltaTime;
				yield return null;
			}

		}

		AudioSource buffer = Instance.MusicSource;
		Instance.MusicSource = CrossFadeSource;
		Instance.CrossFadeSource = buffer;
		currentMusicSource = Instance.MusicSource;
		Instance.CrossFadeSource.Stop ();

		if (MusicEnabled) {
			BaseMusicVolume = _newVolume;
			Instance.CrossFadeSource.volume = 0;
			Instance.MusicSource.volume = BaseMusicVolume;
		}
	}
	
	public static AudioSource PlaySound(string _name, Vector2 _position, float _volume = 1, bool _hasPan = true, Transform _parent = null, bool _isLooped = false) {
		return PlaySound (Instance.soundEffectLib [_name], _position, _volume, _hasPan, _parent, _isLooped);
	}

	public static AudioSource PlaySound(AudioClip _clip, Vector2 _position, float _volume = 1, bool _hasPan = true, Transform _parent = null, bool _isLooped = false) {
		GameObject obj = (GameObject)GameObject.Instantiate (Instance.PlayOnceEffectPrefab);
		obj.transform.parent = _parent != null ? _parent : Instance.transform;
		obj.transform.position = _position;
		Instance.activeSoundEffects.Add (obj);
		return obj.GetComponent<PlayOnceSource> ().Play (_clip, _isLooped, _hasPan, SoundEnabled ? _volume * 0.4f : 0);
	}



}
