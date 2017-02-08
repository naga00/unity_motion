using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 20;
	public GameObject[] balls;
	private float bounce = -1;

	void Start () {
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			float scale = 0.5f;
			ball.GetComponent<Ball>().mass = 30;
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.GetComponent<Ball>().vx = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			balls[i] = ball;
		}
	}
		
	void Update () {
		for(int i=0; i<numBalls; i++){
			GameObject ball = balls[i] as GameObject;
			ball.transform.Translate(ball.GetComponent<Ball>().vx, ball.GetComponent<Ball>().vy, 0);
			CheckWalls(ball);
		}
		for(int i = 0; i < numBalls - 1; i++){
			GameObject ballA = balls[i] as GameObject;
			for(int j=i+1; j<numBalls; j++){
				GameObject ballB = balls[j] as GameObject;
				CheckCollision(ballA, ballB);
			}
		}
	}
		
	private void CheckWalls(GameObject ball) {
		float ballR = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		if(ball.transform.position.x + ballR > GetWorldBottomRight().x){
			ball.transform.position = new Vector2(GetWorldBottomRight().x - ballR, ball.transform.position.y);
			ball.GetComponent<Ball>().vx *= bounce;
		}else if(ball.transform.position.x - ballR < GetWordlTopLeft().x){
			ball.transform.position = new Vector2(GetWordlTopLeft().x + ballR, ball.transform.position.y);
			ball.GetComponent<Ball>().vx *= bounce;
		}

		if(ball.transform.position.y + ballR > GetWordlTopLeft().y){
			ball.transform.position = new Vector2(ball.transform.position.x, GetWordlTopLeft().y - ballR);
			ball.GetComponent<Ball>().vy *= bounce;
		}else if(ball.transform.position.y - ballR < GetWorldBottomRight().y){
			ball.transform.position = new Vector2(ball.transform.position.x, GetWorldBottomRight().y + ballR);
			ball.GetComponent<Ball>().vy *= bounce;
		}
	}

	private void CheckCollision(GameObject ball0, GameObject ball1) {
		float dx = ball1.transform.position.x - ball0.transform.position.x;
		float dy = ball1.transform.position.y - ball0.transform.position.y;
		float dist = Mathf.Sqrt(dx*dx + dy*dy);
		float ballR0 = ball0.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float ballR1 = ball1.GetComponent<SpriteRenderer>().bounds.size.x / 2;

		if(dist < ballR0 + ballR1) {
			float angle = Mathf.Atan2(dy, dx);
			float sine = Mathf.Sin(angle);
			float cosine = Mathf.Cos(angle);
			Vector2 pos0 = new Vector2(0, 0);
			Vector2 pos1 = Rotate(dx, dy, sine, cosine, true);
			Vector2 vel0 = Rotate(ball0.GetComponent<Ball>().vx, ball0.GetComponent<Ball>().vy, sine, cosine, true);
			Vector2 vel1 = Rotate(ball1.GetComponent<Ball>().vx, ball1.GetComponent<Ball>().vy, sine, cosine, true);

			float vxTotal = vel0.x - vel1.x;
			vel0.x = ((ball0.GetComponent<Ball>().mass - ball1.GetComponent<Ball>().mass) * vel0.x + 2 * ball1.GetComponent<Ball>().mass * vel1.x) / (ball0.GetComponent<Ball>().mass + ball1.GetComponent<Ball>().mass);
			vel1.x = vxTotal + vel0.x;

			float absV = Mathf.Abs(vel0.x) + Mathf.Abs(vel1.x);
			float overlap = (ballR0 + ballR1) - Mathf.Abs(pos0.x - pos1.x);
			pos0.x += vel0.x / absV * overlap;
			pos1.x += vel1.x / absV * overlap;

			Vector2 pos0F = Rotate(pos0.x, pos0.y, sine, cosine, false);
			Vector2 pos1F = Rotate(pos1.x, pos1.y, sine, cosine, false);

			ball1.transform.position = new Vector2(ball0.transform.position.x + pos1F.x, ball0.transform.position.y + pos1F.y);
			ball0.transform.position = new Vector2(ball0.transform.position.x + pos0F.x, ball0.transform.position.y + pos0F.y);

			Vector2 vel0F = Rotate(vel0.x, vel0.y, sine, cosine, false);
			Vector2 vel1F = Rotate(vel1.x, vel1.y, sine, cosine,false);
			ball0.GetComponent<Ball>().vx = vel0F.x;
			ball0.GetComponent<Ball>().vy = vel0F.y;
			ball1.GetComponent<Ball>().vx = vel1F.x;
			ball1.GetComponent<Ball>().vy = vel1F.y;
		}
	}

	private Vector2 Rotate(float x, float y, float sine, float cosine, bool reverse) {
		Vector2 result = new Vector2(0, 0);
		if(reverse){
			result.x = x * cosine + y * sine;
			result.y = y * cosine - x * sine;
		}else{
			result.x = x * cosine - y * sine;
			result.y = y * cosine + x * sine;
		}
		return result;  
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