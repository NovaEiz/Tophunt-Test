using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class Rotator : MonoBehaviour {
	
	[Header("Settable fields")]

	[SerializeField] private float _angleY;
	[SerializeField] private float   _angleX    = 60;
	[SerializeField] private float   _distance  = 10;
	[SerializeField] private float   _moveSpeed = 0.5f;

	private void UpdatePosition(GameObject target) {
		if (target == null){
			return;
		}

		float aX = ToRadian(_angleY);
		float aY = ToRadian(_angleX);

		float x = (float)(_distance * Mathf.Sin(aX) * Mathf.Cos(aY));
		float y = (float)(_distance * Mathf.Sin(aY));
		float z = (float)(_distance * Mathf.Cos(aX) * Mathf.Cos(aY));

		Vector3 positionInSphere = new Vector3(x, y, z);

		Vector3 rootPosition = Vector3.zero;

		Vector3 toPosition = rootPosition + positionInSphere;

		float moveToTargetForSeconds = 1f;

		float speedByDistance = Vector3.Distance(target.transform.position, toPosition) * Time.deltaTime / moveToTargetForSeconds;

		target.transform.position = Vector3.MoveTowards(target.transform.position, toPosition, speedByDistance);

		_angleX += _moveSpeed * Time.deltaTime;
		if (_angleX > 359) {
			_angleX -= 360;
		}
	}

	public float GetAngleY() {
		return _angleY;
	}

	private float ToRadian(float angle) {
		return (float)((Mathf.PI * angle) / 180);
	}

	private void LateUpdate() {
		UpdatePosition(gameObject);
	}
}

}
