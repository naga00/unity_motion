﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int COUNT = 20;
	public ArrayList balls;

	void Start () {
		balls = new ArrayList();
		for(int i=0; i<COUNT; i++){
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.position = new Vector2(Random.Range (GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range (GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.transform.localScale = new Vector2(0.2f, 0.2f);
			ball.GetComponent<Ball>().vx = Random.Range(-0.01f, 0.01f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.01f, 0.01f);
			balls.Add(ball);
		}
	}

	private void FixedUpdate () {
		for(int i=0; i<balls.Count; i++) {
			GameObject ball = balls[i] as GameObject;
			ball.transform.position = new Vector2(ball.transform.position.x + ball.GetComponent<Ball>().vx, ball.transform.position.y + ball.GetComponent<Ball>().vy);
			float ballW = ball.GetComponent<SpriteRenderer>().bounds.size.x;
			float ballH = ball.GetComponent<SpriteRenderer>().bounds.size.y;
			if(ball.transform.position.x - ballW/2 < GetWordlTopLeft().x || GetWorldBottomRight().x < ball.transform.position.x + ballW/2 || ball.transform.position.y - ballH/2 < GetWorldBottomRight().y || GetWordlTopLeft().y < ball.transform.position.y + ballH/2) {
				ball.GetComponent<Renderer>().enabled = false;
				balls.Remove(balls[i]);
				if(balls.Count <= 0){
					Time.timeScale = 0;
				}
			}
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