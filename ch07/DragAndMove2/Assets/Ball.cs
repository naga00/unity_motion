﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	public float vx, vy;
	public bool locked;

	void Start() {
		this.gameObject.AddComponent<CircleCollider2D>();
		vx = Random.Range(-0.1f, 0.1f);
		vy = 0.1f;	
	}

	void OnMouseDown() {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		locked = true;
		vx = 0;
		vy = 0;
	}

	void OnMouseDrag() {
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
		transform.position = currentPosition;
	}

	void OnMouseUp() {
		locked = false;
	}
}
