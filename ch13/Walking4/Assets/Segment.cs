using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour {
	public float segmentWidth, segmentHeight;
	public float vx, vy;

	void Start() {
		GameObject segment = this.transform.FindChild("segment").gameObject;
		segmentWidth = segment.GetComponent<SpriteRenderer>().bounds.size.x - segment.transform.position.x / 2;
		segmentHeight = segment.GetComponent<SpriteRenderer>().bounds.size.y - segment.transform.position.y / 2;
	}

	public Vector2 getPin() {
		float angle = this.transform.eulerAngles.z * Mathf.PI / 180;
		float xPos = this.transform.position.x + Mathf.Cos(angle) * segmentWidth;
		float yPos = this.transform.position.y + Mathf.Sin(angle) * segmentWidth;
		return new Vector2(xPos, yPos);  
	}
}