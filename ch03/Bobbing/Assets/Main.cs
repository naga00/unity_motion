using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle;
	private float centerY;
	private float range = 0.5f;
	private float speed = 0.1f;

	void Start () {
		ball = GameObject.Find("ball");
	}

	void Update () {
		ball.transform.position = new Vector2(0, centerY + Mathf.Sin(angle) * range);
		angle += speed;
	}
}