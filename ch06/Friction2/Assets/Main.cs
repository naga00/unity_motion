using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx, vy;
	private float friction = 0.9f;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		vx = Random.Range(-0.5f, 0.5f);
		vy = Random.Range(-0.5f, 0.5f);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}
		
	void Update () {
		vx *= friction;
		vy *= friction;
		ball.transform.Translate(vx, vy, 0);
	}
}