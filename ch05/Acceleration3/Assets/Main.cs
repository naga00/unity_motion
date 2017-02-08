using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx, vy;
	private float ax, ay;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(0, 0);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			ax = -0.001f;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			ax = 0.001f;
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			ay = 0.001f;
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			ay = -0.001f;
		}
		vx += ax;
		vy += ay;
		ball.transform.Translate(vx, vy, 0);
	}
}