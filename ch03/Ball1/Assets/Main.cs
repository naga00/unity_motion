using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;

	void Start () {
		ball = GameObject.Find("ball");
	}

	void Update () {
		ball.transform.position = new Vector2(0, 0);
	}
}