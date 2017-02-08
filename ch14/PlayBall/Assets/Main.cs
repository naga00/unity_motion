using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject segmentPrefab;
	public GameObject[] segments;
	private static readonly int numSegments = 6;
	public GameObject ball;
	private float gravity = -0.01f;
	private float bounce = -0.95f;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		segments = new GameObject[numSegments];
		for (int i = 0; i < numSegments; i++) {
			GameObject segment = Instantiate(segmentPrefab) as GameObject;
			segment.transform.position = new Vector2(0, 0);
			segments[i] = segment;
		}
		ball = GameObject.Find("ball");
		ball.GetComponent<Ball>().vx = 0.1f;
		ball.transform.localScale = new Vector2(1, 1);
		ball.transform.Translate(-1.0f, GetWordlTopLeft().y, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		MoveBall();

		Vector2 target = Reach(segments[0], ball.transform.position.x, ball.transform.position.y);
		for(int i=1; i<numSegments; i++){
			GameObject segment = segments[i];
			target = Reach(segment, target.x, target.y);
		}
		for(int i = numSegments - 1; i > 0; i--){
			GameObject segmentA = segments[i];
			GameObject segmentB = segments[i - 1];
			Position(segmentB, segmentA);
		}

		CheckHit();
	}

	private Vector2 Reach(GameObject segment, float xpos, float ypos) {
		float dx = xpos - segment.transform.position.x;
		float dy = ypos - segment.transform.position.y;
		float angle = Mathf.Atan2(dy, dx);
		segment.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
		float w = segment.GetComponent<Segment>().getPin().x - segment.transform.position.x;
		float h = segment.GetComponent<Segment>().getPin().y - segment.transform.position.y;
		float tx = xpos - w;
		float ty = ypos - h;
		return new Vector2(tx, ty);
	}

	private void Position(GameObject segmentA, GameObject segmentB) {
		segmentA.transform.position = new Vector2(segmentB.GetComponent<Segment>().getPin().x, segmentB.GetComponent<Segment>().getPin().y);
	}

	private void MoveBall() {
		ball.GetComponent<Ball>().vy += gravity;
		ball.transform.Translate(ball.GetComponent<Ball>().vx, ball.GetComponent<Ball>().vy, 0);
		float ballR = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float left = GetWordlTopLeft().x;
		float right = GetWorldBottomRight().x;
		float top = GetWordlTopLeft().y;
		float bottom = GetWorldBottomRight().y;

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

	private void CheckHit() {
		GameObject segment = segments[0];
		float dx = segment.GetComponent<Segment>().getPin().x - ball.transform.position.x;
		float dy = segment.GetComponent<Segment>().getPin().y - ball.transform.position.y;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);
		if(dist < ball.GetComponent<Ball>().radius){
			ball.GetComponent<Ball>().vx += Random.Range(-0.01f, 0.01f);
			ball.GetComponent<Ball>().vy += 0.01f;
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