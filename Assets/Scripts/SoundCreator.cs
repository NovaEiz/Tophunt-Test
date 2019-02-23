using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Nighday {


public class SoundCreator : MonoBehaviour {

	public static SoundCreator Instance;
	
	[SerializeField] private string _pathToSoundsInResources;
		
	[Space]
	[SerializeField] private SoundSystem _soundSystem;

	private void Awake() {
		if (Instance != null) {
			Destroy(this);
			return;
		}
		Instance = this;
	}

	/// <summary>
	/// Sound 2D
	/// </summary>
	/// <param name="name"></param>
	/// <param name="volume"></param>
	/// <param name="cyclical"></param>
	public SoundComponent Create(string name, float volume, bool cyclical) {
		AudioClip clip = GetClip(name);
		
		SoundComponent soundComponent = new SoundComponent(
			clip,
			name,
			volume,
			cyclical
		);
		
		AddSoundComponentToSoundSystem(soundComponent);

		return soundComponent;
	}
	
	/// <summary>
	/// Sound 3D
	/// </summary>
	/// <param name="name"></param>
	/// <param name="volume"></param>
	/// <param name="cyclical"></param>
	/// <param name="target"></param>
	/// <param name="tracking"></param>
	public SoundComponent Create(string name, float volume, bool cyclical, GameObject target, bool tracking) {
		AudioClip clip = GetClip(name);
		
		SoundComponent soundComponent = new SoundComponent(
			clip,
			name,
			volume,
			cyclical,
			target,
			tracking
		);
		
		AddSoundComponentToSoundSystem(soundComponent);

		return soundComponent;
	}

	private AudioClip GetClip(string name) {
		string    path = Path.Combine(_pathToSoundsInResources, name);
		AudioClip clip = Resources.Load<AudioClip>(path);
		return clip;
	}

	private void AddSoundComponentToSoundSystem(SoundComponent soundComponent) {
		_soundSystem.AddSoundComponent(soundComponent);
	}

}

}