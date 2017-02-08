using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 10;
	public GameObject centerBall;
	public GameObject[] balls;
	private float bounce = -1;
	private float spring = 0.2f;

	void Start () {
		centerBall = Instantiate(ballPrefab) as GameObject;
		centerBall.transform.position = new Vector2(0, 0);
		centerBall.transform.localScale = new Vector2(3.0f, 3.0f);
		centerBall.GetComponent<SpriteRenderer>().color = new Color(200/255f, 200/255f, 200/255f, 1);
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			float scale = Random.value + 0.5f;
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.GetComponent<Ball>().vx = Random.Range(-0.01f, 0.01f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.01f, 0.01f);
			ball.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);
			balls[i] = ball;
		}
	}
		
	void Update () {
		for(int i=0; i<balls.Length; i++) {
			GameObject ball = balls[i] as GameObject;
			Move(ball);
			float dx = ball.transform.position.x - centerBall.transform.position.x;
			float dy = ball.transform.position.y - centerBall.transform.position.y;
			float dist = Mathf.Sqrt(dx * dx + dy * dy);
			float minDist = ball.transform.localScale.x/2 + centerBall.transform.localScale.x/2;
			if(dist < minDist) {
				float angle = Mathf.Atan2(dy, dx);
				float tx = centerBall.transform.position.x + Mathf.Cos(angle) * minDist;
				float ty = centerBall.transform.position.y + Mathf.Sin(angle) * minDist;
				ball.GetComponent<Ball>().vx += (tx - ball.transform.position.x) * spring;
				ball.GetComponent<Ball>().vy += (ty - ball.transform.position.y) * spring;
			}
		}
	}

	private void Move(GameObject ball) {
		float ballR = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float left = GetWordlTopLeft().x;
		float right = GetWorldBottomRight().x;
		float top = GetWordlTopLeft().y;
		float bottom = GetWorldBottomRight().y;
		ball.transform.Translate(ball.GetComponent<Ball>().vx, ball.GetComponent<Ball>().vy, 0);

		if(ball.transform.position.x + ballR > right){
			ball.transform.position = new Vector2(right - ballR, ball.transform.position.y);
			ball.GetComponent<Ball>().vx *= bounce;
		}else if(ball.transform.position.x - ballR < left){
			ball.transform.position = new Vector2(left + ballR, ball.transform.position.y);
			ball.GetComponent<Ball>().vx *= bounce;
		}

		if(ball.transform.position.y - ballR < bottom){
			ball.transform.position = new Vector2(ball.transform.position.x, bottom + ballR);
			ball.GetComponent<Ball>().vy *= bounce;
		}else if(ball.transform.position.y + ballR > top){
			ball.transform.position = new Vector2(ball.transform.position.x, top - ballR);
			ball.GetComponent<Ball>().vy *= bounce;
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