using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	public float vx, vy;
	public float oldX, oldY;
	public bool isDragging;

	void Start() {
		this.gameObject.AddComponent<CircleCollider2D>();
	}

	void OnMouseDown() {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		oldX = transform.position.x;
		oldY = transform.position.y;
		isDragging = true;
	}

	void OnMouseDrag() {
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
		transform.position = currentPosition;
	}

	void OnMouseUp() {
		isDragging= false;
	}

	public void trackVelocity() {
		vx = transform.position.x - oldX;
		vy = transform.position.y - oldY;
		oldX = transform.position.x;
		oldY = transform.position.y;
	}
}
