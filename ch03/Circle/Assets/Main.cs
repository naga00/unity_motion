using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle;
	private float centerX, centerY;
	private float radius = 2;
	private float speed = 0.05f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(centerX, centerY);
	}

	void Update () {
		ball.transform.position = new Vector2(centerX + Mathf.Sin(angle) * radius, centerY + Mathf.Cos(angle) * radius);
		angle -= speed;
	}
}