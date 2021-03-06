﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float gravity = -0.01f;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		if (ball.GetComponent<Ball>().locked) return;
		float ballR = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float left = GetWordlTopLeft().x;
		float right = GetWorldBottomRight().x;
		float top = GetWordlTopLeft().y;
		float bottom = GetWorldBottomRight().y;
		ball.GetComponent<Ball>().vy += gravity;
		ball.transform.Translate(ball.GetComponent<Ball>().vx, ball.GetComponent<Ball>().vy, 0);

		if(ball.transform.position.x + ballR > right){
			ball.transform.position = new Vector2(right - ballR, ball.transform.position.y);
			ball.GetComponent<Ball>().vx *= -1;
		}else if(ball.transform.position.x - ballR < left){
			ball.transform.position = new Vector2(left + ballR, ball.transform.position.y);
			ball.GetComponent<Ball>().vx *= -1;
		}

		if(ball.transform.position.y - ballR < bottom){
			ball.transform.position = new Vector2(ball.transform.position.x, bottom + ballR);
			ball.GetComponent<Ball>().vy *= -1;
		}else if(ball.transform.position.y + ballR > top){
			ball.transform.position = new Vector2(ball.transform.position.x, top - ballR);
			ball.GetComponent<Ball>().vy *= -1;
		}
	}

	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1, -1, 1));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1, -1, 1));
		return bottomRight;
	}
}