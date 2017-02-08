using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vr = 0.05f;
	private float cosine;
	private float sine;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		cosine = Mathf.Cos(vr);
		sine = Mathf.Sin(vr);
	}
		
	void Update () {
		float x1 = ball.transform.position.x;
		float y1 = ball.transform.position.y;
		float x2 = cosine * x1 - sine * y1;
		float y2 = cosine * y1 + sine * x1;
		ball.transform.position = new Vector2(x2, y2);
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