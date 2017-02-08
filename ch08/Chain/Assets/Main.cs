using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball0, ball1, ball2;
	private float spring = 0.05f;
	private float vx, vy;
	private float friction = 0.8f;
	private float gravity = 0.1f;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		this.gameObject.AddComponent<LineRenderer>();
		ball0 = GameObject.Find("ball0") as GameObject;
		ball0.transform.position = new Vector2(0, 0);
		ball0.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		ball1 = GameObject.Find("ball1") as GameObject;
		ball1.transform.position = new Vector2(0, 0);
		ball1.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		ball2 = GameObject.Find("ball2") as GameObject;
		ball2.transform.position = new Vector2(0, 0);
		ball2.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		MoveBall(ball0, worldMousePosition.x, worldMousePosition.y);
		MoveBall(ball1, ball0.transform.position.x, ball0.transform.position.y);
		MoveBall(ball2, ball1.transform.position.x, ball1.transform.position.y);
		DrawLine(1, new Vector2(worldMousePosition.x, worldMousePosition.y), new Vector2(ball0.transform.position.x, ball0.transform.position.y));
		DrawLine(3, new Vector2(ball0.transform.position.x, ball0.transform.position.y), new Vector2(ball1.transform.position.x, ball1.transform.position.y));
		DrawLine(5, new Vector2(ball1.transform.position.x, ball1.transform.position.y), new Vector2(ball2.transform.position.x, ball2.transform.position.y));
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
		lineRenderer.SetVertexCount(6);
		lineRenderer.SetPosition(index-1, v0);
		lineRenderer.SetPosition(index, v1);
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