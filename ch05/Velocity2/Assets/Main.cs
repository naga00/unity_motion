using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx = 0.1f;
	private float vy = -0.1f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(GetWordlTopLeft().x, 0);
	}

	void Update () {
		ball.transform.Translate(vx, vy, 0);
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