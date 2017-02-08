using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx, vy;
	private float friction = 0.01f;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		vx = Random.Range(-0.3f, 0.3f);
		vy = Random.Range(-0.3f, 0.3f);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}
		
	void Update () {
		float speed = Mathf.Sqrt(vx * vx + vy * vy);
		float angle = Mathf.Atan2(vy, vx);
		if (speed > friction){
			speed -= friction;
		}else{
			speed = 0;
		}
		vx = Mathf.Cos(angle) * speed;
		vy = Mathf.Sin(angle) * speed;
		ball.transform.Translate(vx, vy, 0);
	}
}