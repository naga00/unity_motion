using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle;
	private float centerScale = 4;
	private float range = 1;
	private float speed = 0.1f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(0, 0);
		ball.transform.localScale = new Vector2(1, 1);
	}

	void Update () {
		float scaleX = centerScale + Mathf.Sin(angle) * range;
		float scaleY = scaleX;
		ball.transform.localScale = new Vector2(scaleX, scaleY);
		angle += speed;
	}
}