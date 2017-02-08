using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx, vy;
	private float gravity = -0.1f;
	private float bounce = -0.7f;
	private float time;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.localScale = new Vector2(1, 1);
		ball.transform.position = new Vector2(0, 0);
		vx = Random.Range(-0.1f, 0.1f);
		vy = 0.1f;
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		time = Time.time;
	}
		
	void Update () {
		float elapsed = Time.time - time;
		time = Time.time;
		vy += gravity * elapsed;
		ball.transform.Translate(vx, vy, 0);

		float ballR = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float left = GetWordlTopLeft().x;
		float right = GetWorldBottomRight().x;
		float top = GetWordlTopLeft().y;
		float bottom = GetWorldBottomRight().y;
		if(ball.transform.position.x + ballR > right){
			ball.transform.position = new Vector2(right - ballR, ball.transform.position.y);
			vx *= bounce;
		}else if(ball.transform.position.x - ballR < left){
			ball.transform.position = new Vector2(left + ballR, ball.transform.position.y);
			vx *= bounce;
		}
		if(ball.transform.position.y - ballR < bottom){
			ball.transform.position = new Vector2(ball.transform.position.x, bottom + ballR);
			vy *= bounce;
		}else if(ball.transform.position.y + ballR > top){
			ball.transform.position = new Vector2(ball.transform.position.x, top - ballR);
			vy *= bounce;
		}
	}

	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1, -1, 1));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1, -1, 1));
		return bottomRight;
	}
}