using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float angle;
	private float radius = 2;
	private float vr = 0.05f;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}
		
	void Update () {
		ball.transform.position = new Vector2(Mathf.Cos(angle)*radius, Mathf.Sin(angle)*radius);
		angle -= vr;
	}
}