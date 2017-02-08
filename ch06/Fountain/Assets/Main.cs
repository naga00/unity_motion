using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	private static readonly int COUNT = 100;
	public GameObject ballPrefab;
	public GameObject[] balls;
	private float gravity = -0.005f;

	void Start () {
		balls = new GameObject[COUNT];
		for (int i = 0; i < COUNT; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.localScale = new Vector2(0.1f, 0.1f);
			ball.transform.position = new Vector2(0, GetWorldBottomRight().y);
			ball.GetComponent<Ball>().vx = Random.Range(-0.01f, 0.01f);
			ball.GetComponent<Ball>().vy = Random.Range(0.1f, 0.3f);
			ball.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);
			balls[i] = ball;
		}
	}
		
	void Update () {
		for(int i=0; i<balls.Length; i++) {
			GameObject ball = balls[i] as GameObject;
			ball.GetComponent<Ball>().vy += gravity;
			ball.transform.position = new Vector2(ball.transform.position.x + ball.GetComponent<Ball>().vx, ball.transform.position.y + ball.GetComponent<Ball>().vy);
			float ballW = ball.GetComponent<SpriteRenderer>().bounds.size.x;
			float ballH = ball.GetComponent<SpriteRenderer>().bounds.size.y;
			if(ball.transform.position.x - ballW/2 < GetWordlTopLeft().x || GetWorldBottomRight().x < ball.transform.position.x + ballW/2 || ball.transform.position.y - ballH/2 < GetWorldBottomRight().y || GetWordlTopLeft().y < ball.transform.position.y + ballH/2) {
				ball.transform.position = new Vector2(0, GetWorldBottomRight().y);
				ball.GetComponent<Ball>().vx = Random.Range(-0.01f, 0.01f);
				ball.GetComponent<Ball>().vy = Random.Range(0.1f, 0.3f);
			}
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