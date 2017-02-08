using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float easing = 0.05f;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		Vector3 worldMousePoin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float vx = (worldMousePoin.x - ball.transform.position.x) * easing;
		float vy = (worldMousePoin.y - ball.transform.position.y) * easing;
		ball.transform.Translate(vx, vy, 0);
	}
}