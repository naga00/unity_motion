﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle;
	private float centerX, centerY;
	private float radiusX = 3.5f;
	private float radiusY = 1.5f;
	private float speed = 0.1f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(centerX, centerY);
	}

	void Update () {
		ball.transform.position = new Vector2(centerX + Mathf.Sin(angle) * radiusX, centerY + Mathf.Cos(angle) * radiusY);
		angle -= speed;
	}
}