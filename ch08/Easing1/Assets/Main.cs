using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float easing = 0.02f;
	private float targetX, targetY;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(GetWordlTopLeft().x, GetWordlTopLeft().y);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		targetX = 0;
		targetY = 0;
	}

	void Update () {
		float vx = (targetX - ball.transform.position.x) * easing;
		float vy = (targetY - ball.transform.position.y) * easing;
		ball.transform.Translate(vx, vy, 0);
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