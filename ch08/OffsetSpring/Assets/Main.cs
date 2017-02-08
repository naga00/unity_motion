using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float spring = 0.1f;
	private float vx, vy;
	private float friction = 0.95f;
	private float springLength = 3;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float dx = ball.transform.position.x - worldMousePosition.x;
		float dy = ball.transform.position.y - worldMousePosition.y;
		float angle = Mathf.Atan2(dy, dx);
		float targetX = worldMousePosition.x + Mathf.Cos(angle) * springLength;
		float targetY = worldMousePosition.y + Mathf.Sin(angle) * springLength;
		vx += (targetX - ball.transform.position.x) * spring;
		vy += (targetY - ball.transform.position.y) * spring;
		vx *= friction;
		vy *= friction;
		ball.transform.Translate(vx, vy, 0);
		DrawLine(new Vector2(ball.transform.position.x, ball.transform.position.y), new Vector2(worldMousePosition.x, worldMousePosition.y));
	}

	private void DrawLine(Vector2 v0, Vector2 v1){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.sortingOrder = -1;
		lineRenderer.material = material;
		lineRenderer.SetColors(Color.black, Color.black);
		lineRenderer.SetWidth(0.02f, 0.02f);	
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, v0);
		lineRenderer.SetPosition(1, v1);	
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