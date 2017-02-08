using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numHandles = 3;
	public GameObject ball;
	public GameObject[] handles;
	private float spring = 0.07f;
	private float friction = 0.8f;
	private float gravity = 0.1f;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		this.gameObject.AddComponent<LineRenderer>();
		handles = new GameObject[numHandles];
		ball = Instantiate(ballPrefab) as GameObject;
		ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		for (int i = 0; i < numHandles; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.localScale = new Vector2(0.5f, 0.5f);
			ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			ball.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
			handles[i] = ball;
		}
	}

	void Update () {
		for(int i = 0; i < numHandles; i++){
			GameObject handle = handles[i];
			float dx = handle.transform.position.x - ball.transform.position.x;
			float dy = handle.transform.position.y - ball.transform.position.y;
			ball.GetComponent<Ball>().vx += dx * spring;
			ball.GetComponent<Ball>().vy += dy * spring; 
		}
		ball.GetComponent<Ball>().vx *= friction;
		ball.GetComponent<Ball>().vy *= friction;
		ball.transform.Translate(ball.GetComponent<Ball>().vx, ball.GetComponent<Ball>().vy, 0, Space.World);
		for(int i=0; i<numHandles; i++) {
			DrawLine(i, new Vector2(ball.transform.position.x, ball.transform.position.y), new Vector2(handles[i].transform.position.x, handles[i].transform.position.y));
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
		lineRenderer.SetWidth(0.02f, 0.02f);	
		lineRenderer.SetVertexCount(numHandles * 2);
		lineRenderer.SetPosition(index * 2, v0);
		lineRenderer.SetPosition(index * 2 + 1, v1);
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