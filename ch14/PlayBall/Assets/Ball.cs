using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public float vx, vy;
	public float radius;

	void Start () {
		radius = this.GetComponent<SpriteRenderer>().bounds.size.x / 2;
	}
}