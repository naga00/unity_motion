using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 30;
	public GameObject[] balls;
	private float bounce = -0.5f;
	private float spring = 0.5f;
	private float gravity = -0.01f;

	void Start () {
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			float scale = Random.Range(0.2f, 1.0f);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.GetComponent<Ball>().vx = Random.Range(-0.01f, 0.01f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.01f, 0.01f);
			ball.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);
			balls[i] = ball;
		}
	}
		
	void Update () {
		for(int i=0; i<numBalls-1; i++){
			GameObject ball0 = balls[i] as GameObject;
			for(int j=i+1; j<numBalls; j++){
				GameObject ball1 = balls[j] as GameObject;
				float dx = ball1.transform.position.x - ball0.transform.position.x;
				float dy = ball1.transform.position.y - ball0.transform.position.y;
				float dist = Mathf.Sqrt(dx * dx + dy * dy);
				float minDist = ball0.GetComponent<SpriteRenderer>().bounds.size.x / 2 + ball1.GetComponent<SpriteRenderer>().bounds.size.x / 2;
				if(dist < minDist){
					float tx = ball0.transform.position.x + dx / dist * minDist;
					float ty = ball0.transform.position.y + dy / dist * minDist;
					float ax = (tx - ball1.transform.position.x) * spring;
					float ay = (ty - ball1.transform.position.y) * spring;
					ball0.GetComponent<Ball>().vx -= ax;
					ball0.GetComponent<Ball>().vy -= ay;
					ball1.GetComponent<Ball>().vx += ax;
					ball1.GetComponent<Ball>().vy += ay;
				}
			}
		}

		for(int i=0; i<numBalls; i++) {
			GameObject ball = balls[i] as GameObject;
			move(ball);
		}
	}

	void move(GameObject ball) {
		float ballR = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float left = GetWordlTopLeft().x;
		float right = GetWorldBottomRight().x;
		float top = GetWordlTopLeft().y;
		float bottom = GetWorldBottomRight().y;
		ball.GetComponent<Ball>().vy += gravity;
		ball.transform.position = new Vector2(ball.transform.position.x + ball.GetComponent<Ball>().vx, ball.transform.position.y + ball.GetComponent<Ball>().vy);

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