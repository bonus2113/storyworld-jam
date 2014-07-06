using UnityEngine;

public class PlayOnceSource : MonoBehaviour {

	AudioSource source;
	float timer = 0;

	public AudioSource Play(AudioClip _clip, bool _isLooped, bool _hasPan, float _volume) {
		source = GetComponent<AudioSource> ();
		source.clip = _clip;
		source.loop = _isLooped;
		source.volume = _volume;
		source.panLevel = _hasPan ? 1 : 0;
		source.Play ();
		return source;
	}

	void Update() {
		if(source) {
			timer += Time.deltaTime;
			if(!source.isPlaying && timer > 0.1f)
				GameObject.Destroy(gameObject);
		}
	}
}
