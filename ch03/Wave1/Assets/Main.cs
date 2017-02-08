using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle;
	private float centerY;
	private float range = 1;
	private float xspeed = 0.05f;
	private float yspeed = 0.05f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(GetWordlTopLeft().x - 1.5f, 0);
	}

	void Update () {
		ball.transform.position = new Vector2(ball.transform.position.x + xspeed, centerY + Mathf.Sin(angle) * range);
		angle += yspeed;
	}

	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1f, -1f, 1f));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1f, -1f, 1f));
		return bottomRight;
	}
}