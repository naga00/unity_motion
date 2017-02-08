using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 5;
	public GameObject[] balls;
	private float spring = 0.07f;
	private float friction = 0.8f;
	private float gravity = 0.1f;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		this.gameObject.AddComponent<LineRenderer>();
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.GetComponent<Ball>().vx = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			balls[i] = ball;
		}
	}

	void Update () {
		Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		MoveBall(balls[0], worldMousePosition.x, worldMousePosition.y); 
		for(int i=1; i<numBalls; i++) {
			GameObject ballA = balls[i-1];
			GameObject ballB = balls[i]; 
			MoveBall(ballB, ballA.transform.position.x, ballA.transform.position.y);
		}
		DrawLine(1, new Vector2(worldMousePosition.x, worldMousePosition.y), new Vector2(balls[0].transform.position.x, balls[0].transform.position.y));
		for(int i=1; i<numBalls; i++) {
			GameObject ballA = balls[i-1];
			GameObject ballB = balls[i];
			DrawLine(i+1, new Vector2(ballA.transform.position.x, ballA.transform.position.y), new Vector2(ballB.transform.position.x, ballB.transform.position.y));
		}
	}

	private void MoveBall(GameObject ball, float targetX, float targetY) {
		float vx = ball.GetComponent<Ball>().vx;
		float vy = ball.GetComponent<Ball>().vy;
		ball.GetComponent<Ball>().vx += (targetX - ball.transform.position.x) * spring;
		ball.GetComponent<Ball>().vy += (targetY - ball.transform.position.y) * spring;
		ball.GetComponent<Ball>().vy -= gravity;
		ball.GetComponent<Ball>().vx *= friction;
		ball.GetComponent<Ball>().vy *= friction;
		ball.transform.Translate(ball.GetComponent<Ball>().vx, ball.GetComponent<Ball>().vy, 0, Space.World);
	}

	private void DrawLine(int index, Vector2 v0, Vector2 v1){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.sortingOrder = -1;
		lineRenderer.material = material;
		lineRenderer.SetColors(Color.black, Color.black);
		lineRenderer.SetWidth(0.01f, 0.01f);	
		lineRenderer.SetVertexCount(numBalls + 1);
		lineRenderer.SetPosition(index - 1, v0);
		lineRenderer.SetPosition(index, v1);
	}

	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1f, -1f, 1f));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1f, -1f, 1f));
		return bottomRight;
	}
}