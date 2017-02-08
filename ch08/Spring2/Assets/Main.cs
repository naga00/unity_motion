using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float spring = 0.05f;
	private float targetX, targetY;
	private float vx, vy;
	private float friction = 0.95f;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(GetWordlTopLeft().x, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		targetX = 0;
	}

	void Update () {
		float dx = targetX - ball.transform.position.x;
		float ax = dx * spring;
		vx += ax;
		vx *= friction;
		ball.transform.Translate(vx, 0, 0);
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