using System;
using System.Collections;
using System.Collections.Generic;
using Nighday;
using UnityEngine;

namespace Nighday {

public class Test : MonoBehaviour {

	[SerializeField] private GameObject _target;
	[SerializeField] private string _nameSound;

	private SoundComponent _soundComponentLink;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.X)) {
			SoundCreator.Instance.Create(_nameSound, 1, false);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			SoundCreator.Instance.Create(_nameSound, 1, true);
		}
		if (Input.GetKeyDown(KeyCode.V)) {
			SoundCreator.Instance.Create(_nameSound, 1, false, _target, false);
		}
		if (Input.GetKeyDown(KeyCode.B)) {
			SoundCreator.Instance.Create(_nameSound, 1, false, _target, true);
		}
		if (Input.GetKeyDown(KeyCode.N)) {
			SoundCreator.Instance.Create(_nameSound, 1, true, _target, false);
		}
		if (Input.GetKeyDown(KeyCode.M)) {
			_soundComponentLink = SoundCreator.Instance.Create(_nameSound, 1, true, _target, true);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			if (_soundComponentLink != null) {
				_soundComponentLink.Stop();
				_soundComponentLink = null;
			}
		}
	}



}

}