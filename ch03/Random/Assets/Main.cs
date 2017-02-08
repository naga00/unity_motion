using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float centerX, centerY;
	private float angleX, angleY;
	private float range = 2;
	private float xspeed = 0.05f;
	private float yspeed = 0.1f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(centerX, centerY);
	}

	void Update () {
		ball.transform.position = new Vector2(centerX + Mathf.Sin(angleX) * range, centerY + Mathf.Sin(angleY) * range);
		angleX += xspeed;
		angleY += yspeed;
	}
}