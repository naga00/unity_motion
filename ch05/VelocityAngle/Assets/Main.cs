using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle = -45.0f;
	private float speed = 0.05f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(GetWordlTopLeft().x, 1.5f);
		ball.transform.localScale = new Vector2(1, 1);
	}

	void Update () {
		float radians = angle * Mathf.PI / 180;
		float vx = Mathf.Cos(radians) * speed;
		float vy = Mathf.Sin(radians) * speed;
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