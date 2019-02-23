using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class SoundSystem : MonoBehaviour {

	private List<SoundComponent> _sounds;
	private List<SoundComponent> _forRemoveFromListSounds;

	private string _additionalNameStaticSound = " [Static Sound]";

	private void Awake() {
		_sounds = new List<SoundComponent>();
		_forRemoveFromListSounds = new List<SoundComponent>();
	}

	public void AddSoundComponent(SoundComponent soundComponent) {
		InitComponent(soundComponent);
		_sounds.Add(soundComponent);
	}

	private void InitAudioSource(SoundComponent soundComponent) {
		AudioClip clip = soundComponent.AudioClip;
		GameObject target = soundComponent.Target;
		bool tracking = soundComponent.Tracking;
		
		if (target == null) {
			target = gameObject;
		} else {
			if (!tracking) {
				Vector3 position = target.transform.position;
				target                    = new GameObject(target.name + _additionalNameStaticSound);
				target.transform.position = position;
			}
			soundComponent.SetTarget(target);
		}
		AudioSource audioSource = target.AddComponent<AudioSource>();
		audioSource.clip = clip;

		if (soundComponent.SoundType == SoundType.Sound3D) {
			audioSource.spatialBlend = 1;
		}
		
		soundComponent.SetAudioSource(audioSource);
	}

	private void Update() {
		var len = _sounds.Count;
		for (var i=0; i<len; i++) {
			var sound = _sounds[i];
			if (sound.AudioSource == null || (sound.SoundType == SoundType.Sound3D && sound.Target == null)) {
				//Если удалить в Unity редакторе AudioSource, или GameObject(Target) для статичного звука.
				//Этот блок условия можно убрать, если не будет вмешательства в редакторе
				
				_sounds.RemoveAt(i);
				i--;
				len--;
				if (!sound.Tracking && sound.Target != null) {
					Destroy(sound.Target);
				} else if (sound.AudioSource != null) {
					Destroy(sound.AudioSource);
				}
				continue;
			}
			OnUpdateComponent(sound);
		}

		if (_forRemoveFromListSounds.Count > 0) {
			foreach (var item in _forRemoveFromListSounds) {
				_sounds.Remove(item);
			}
			_forRemoveFromListSounds.Clear();
		}
	}

	private void InitComponent(SoundComponent soundComponent) {
		InitAudioSource(soundComponent);
		soundComponent.AudioSource.loop = soundComponent.Loop;
		soundComponent.AudioSource.volume = soundComponent.Volume;
		
		soundComponent.AddOnStop(StopSound);
	}

	private void OnUpdateComponent(SoundComponent soundComponent) {
		if (soundComponent.Started) {
			if (!soundComponent.Loop && !soundComponent.AudioSource.isPlaying) {//Если компонент не зациклен и звук закончил воспроизведение
				DestroySound(soundComponent);
			}
			return;
		}
		
		soundComponent.AudioSource.Play();
		soundComponent.SetStarted(true);
	}

	private void DestroySound(SoundComponent soundComponent) {
		if (soundComponent.Target != null) {
			if (!soundComponent.Tracking) { //Если нет слежения, то надо уничтожить GameObject, т.к. он создан специально для статичного звука
				Destroy(soundComponent.Target);
			} else {
				Destroy(soundComponent.AudioSource);
			}
		} else {
			Destroy(soundComponent.AudioSource); //Так как нет цели, надо уничтожить только AudioSource
		}
		RemoveItemFromList(soundComponent);
	}

	private void RemoveItemFromList(SoundComponent soundComponent) {
		_forRemoveFromListSounds.Add(soundComponent);
	}

	private void StopSound(SoundComponent soundComponent) {
		DestroySound(soundComponent);
	}
	
}

}