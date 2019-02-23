using System;
using UnityEngine;

namespace Nighday {

public enum SoundType {
	Sound2D,
	Sound3D
}

public class SoundComponent {
	private AudioSource _audioSource;
	private AudioClip _audioClip;
	private string _name;
	private float _volume;
	private bool _loop;
	
	private GameObject _target;
	private bool _tracking;

	private SoundType _soundType;
	
	private bool _started;

	public AudioSource AudioSource => _audioSource;
	public AudioClip AudioClip => _audioClip;
	public string Name => _name;
	public float Volume => _volume;
	public bool Loop => _loop;
	
	public GameObject Target => _target;
	public bool Tracking => _tracking;
	
	public bool Started => _started;
	public SoundType SoundType => _soundType;

	public SoundComponent(AudioClip audioClip, string name, float volume, bool loop) {
		SetFieldsSound2D(audioClip, name, volume, loop);
		_soundType = SoundType.Sound2D;
	}
	public SoundComponent(AudioClip audioClip, string name, float volume, bool loop, GameObject target, bool tracking) {
		SetFieldsSound3D(audioClip, name, volume, loop, target, tracking);
		_soundType = SoundType.Sound3D;
	}

	private void SetFieldsSound2D(AudioClip audioClip, string name, float volume, bool loop) {
		_audioClip = audioClip;
		_name      = name;
		_volume    = volume;
		_loop      = loop;
	}
	private void SetFieldsSound3D(AudioClip audioClip, string name, float volume, bool loop, GameObject target, bool tracking) {
		SetFieldsSound2D(audioClip, name, volume, loop);
		
		_target   = target;
		_tracking = tracking;
	}

#region Methods

	public void SetAudioSource(AudioSource value) {
		_audioSource = value;
	}
	public void SetTarget(GameObject value) {
		_target = value;
	}
	public void SetStarted(bool value) {
		_started = value;
	}
	
#endregion
	
#region Events
	
	private Action<SoundComponent> _onStop;
	
	public void AddOnStop(Action<SoundComponent> callback) {
		_onStop += callback;
	}
	public void RemoveOnStop(Action<SoundComponent> callback) {
		if (_onStop != null) {
			_onStop -= callback;
		}
	}

	public void Stop() {
		if (_onStop != null) {
			_onStop(this);
		}
	}
	
#endregion


}

}