using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball0, ball1;
	private float bounce = -1;

	void Start () {
		ball0 = GameObject.Find("ball0") as GameObject;
		ball0.GetComponent<Ball>().mass = 2;
		ball0.GetComponent<Ball>().vx = Random.Range(-0.05f, 0.05f);
		ball0.GetComponent<Ball>().vy = Random.Range(-0.05f, 0.05f);
		ball0.transform.localScale = new Vector2(4, 4);
		ball0.transform.position = new Vector2(-2, 0);
		ball0.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

		ball1 = GameObject.Find("ball1") as GameObject;
		ball1.GetComponent<Ball>().mass = 1;
		ball1.GetComponent<Ball>().vx = Random.Range(-0.05f, 0.05f);
		ball1.GetComponent<Ball>().vy = Random.Range(-0.05f, 0.05f);
		ball1.transform.localScale = new Vector2(1, 1);
		ball1.transform.position = new Vector2(2, 0);
		ball1.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}
		
	void Update () {
		ball0.transform.Translate(ball0.GetComponent<Ball>().vx, ball0.GetComponent<Ball>().vy, 0);
		ball1.transform.Translate(ball1.GetComponent<Ball>().vx, ball1.GetComponent<Ball>().vy, 0);
		CheckCollision(ball0, ball1);
		CheckWalls(ball0);
		CheckWalls(ball1);	
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
			Vector2 pos1 = rotate(dx, dy, sine, cosine, true);
			Vector2 vel0 = rotate(ball0.GetComponent<Ball>().vx, ball0.GetComponent<Ball>().vy, sine, cosine, true);
			Vector2 vel1 = rotate(ball1.GetComponent<Ball>().vx, ball1.GetComponent<Ball>().vy, sine, cosine, true);

			float vxTotal = vel0.x - vel1.x;
			vel0.x = ((ball0.GetComponent<Ball>().mass - ball1.GetComponent<Ball>().mass) * vel0.x + 2 * ball1.GetComponent<Ball>().mass * vel1.x) / (ball0.GetComponent<Ball>().mass + ball1.GetComponent<Ball>().mass);
			vel1.x = vxTotal + vel0.x;

			float absV = Mathf.Abs(vel0.x) + Mathf.Abs(vel1.x);
			float overlap = (ballR0 + ballR1) - Mathf.Abs(pos0.x - pos1.x);
			pos0.x += vel0.x / absV * overlap;
			pos1.x += vel1.x / absV * overlap;

			Vector2 pos0F = rotate(pos0.x, pos0.y, sine, cosine, false);
			Vector2 pos1F = rotate(pos1.x, pos1.y, sine, cosine, false);
			ball1.transform.position = new Vector2(ball0.transform.position.x + pos1F.x, ball0.transform.position.y + pos1F.y);
			ball0.transform.position = new Vector2(ball0.transform.position.x + pos0F.x, ball0.transform.position.y + pos0F.y);

			Vector2 vel0F = rotate(vel0.x, vel0.y, sine, cosine, false);
			Vector2 vel1F = rotate(vel1.x, vel1.y, sine, cosine,false);
			ball0.GetComponent<Ball>().vx = vel0F.x;
			ball0.GetComponent<Ball>().vy = vel0F.y;
			ball1.GetComponent<Ball>().vx = vel1F.x;
			ball1.GetComponent<Ball>().vy = vel1F.y;
		}
	}

	private Vector2 rotate(float x, float y, float sine, float cosine, bool reverse) {
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