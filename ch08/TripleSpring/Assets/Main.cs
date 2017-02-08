using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball0, ball1, ball2;
	private float spring = 0.1f;
	private float friction = 0.9f;
	private float springLength = 3;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		ball0 = GameObject.Find("ball0") as GameObject;
		ball0.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
		ball0.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		ball1 = GameObject.Find("ball1") as GameObject;
		ball1.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
		ball1.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		ball2 = GameObject.Find("ball2") as GameObject;
		ball2.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
		ball2.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		if (!ball0.GetComponent<Ball>().isDragging) {
			SpringTo(ball0, ball1);
			SpringTo(ball0, ball2);
		}
		if (!ball1.GetComponent<Ball>().isDragging) {
			SpringTo(ball1, ball0);
			SpringTo(ball1, ball2);
		}
		if (!ball2.GetComponent<Ball>().isDragging) {
			SpringTo(ball2, ball0);
			SpringTo(ball2, ball1);
		}
		DrawLine(0, new Vector2(ball0.transform.position.x, ball0.transform.position.y), new Vector2(ball1.transform.position.x, ball1.transform.position.y));
		DrawLine(2, new Vector2(ball1.transform.position.x, ball1.transform.position.y), new Vector2(ball2.transform.position.x, ball2.transform.position.y));
		DrawLine(4, new Vector2(ball2.transform.position.x, ball2.transform.position.y), new Vector2(ball0.transform.position.x, ball0.transform.position.y));
	}

	private void SpringTo(GameObject ballA, GameObject ballB) {
		float dx = ballB.transform.position.x - ballA.transform.position.x;
		float dy = ballB.transform.position.y - ballA.transform.position.y;
		float angle = Mathf.Atan2(dy, dx);
		float targetX = ballB.transform.position.x - Mathf.Cos(angle) * springLength;
		float targetY = ballB.transform.position.y - Mathf.Sin(angle) * springLength;
		ballA.GetComponent<Ball>().vx += (targetX - ballA.transform.position.x) * spring;
		ballA.GetComponent<Ball>().vy += (targetY - ballA.transform.position.y) * spring;
		ballA.GetComponent<Ball>().vx *= friction;
		ballA.GetComponent<Ball>().vy *= friction;
		ballA.transform.Translate(ballA.GetComponent<Ball>().vx, ballA.GetComponent<Ball>().vy, 0, Space.World);
	}
		
	private void DrawLine(int index, Vector2 v0, Vector2 v1){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.sortingOrder = -1;
		lineRenderer.material = material;
		lineRenderer.SetColors(Color.black, Color.black);
		lineRenderer.SetWidth(0.02f, 0.02f);	
		lineRenderer.SetVertexCount(6);
		lineRenderer.SetPosition(index, v0);
		lineRenderer.SetPosition(index + 1, v1);	
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