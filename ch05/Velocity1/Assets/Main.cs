using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx = 0.1f;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(0, 0);
	}

	void Update () {
		ball.transform.Translate(vx, 0, 0);
	}
}