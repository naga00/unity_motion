using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 10;
	public GameObject[] balls;

	void Start () {
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			balls[i] = ball;
		}
	}
		
	void Update () {
		Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float angle = - worldMousePosition.x * 0.01f;
		float cosine = Mathf.Cos(angle);
		float sine = Mathf.Sin(angle);

		for(int i=0; i<numBalls; i++) {
			GameObject ball = balls[i] as GameObject;
			float x1 = ball.transform.position.x;
			float y1 = ball.transform.position.y;
			float x2 = cosine * x1 - sine * y1;
			float y2 = cosine * y1 + sine * x1;
			ball.transform.position = new Vector2(x2, y2);
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