using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject lineObj, ballObj;
	public GameObject bar;
	private float gravity = -0.01f;
	private float bounce = -0.6f;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		lineObj = GameObject.Find("linePrefab");
		bar = lineObj.transform.FindChild("bar").gameObject;
		bar.GetComponent<SpriteRenderer>().material.color = Color.black;
		lineObj.transform.position = new Vector2(-8.7f, 1);
		lineObj.transform.Rotate(new Vector3(0, 0, -30));

		ballObj =  GameObject.Find("ballPrefab");
		ballObj.GetComponent<SpriteRenderer>().material.color = Color.red;
		ballObj.transform.position = new Vector2(GetWordlTopLeft().x + 1, GetWordlTopLeft().y - 1);
		ballObj.transform.localScale = new Vector2(1.5f, 1.5f);
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lineObj.transform.rotation = Quaternion.Euler(0, 0, worldMousePosition.x * 5 );

		ballObj.GetComponent<Ball>().vy += gravity;
		ballObj.transform.Translate(ballObj.GetComponent<Ball>().vx, ballObj.GetComponent<Ball>().vy, 0, Space.World);
		float left = lineObj.transform.position.x;
		float right = lineObj.transform.position.x + bar.GetComponent<SpriteRenderer>().bounds.size.x;

		if (ballObj.transform.position.x > left && ballObj.transform.position.x < right) {
			float angle = lineObj.transform.eulerAngles.z * Mathf.PI / 180;
			float cosine = Mathf.Cos(angle);
			float sine = Mathf.Sin(angle);
			float x1 = ballObj.transform.position.x - lineObj.transform.position.x;
			float y1 = ballObj.transform.position.y - lineObj.transform.position.y;
			float y2 = cosine * y1 - sine * x1;

			if (y2 < ballObj.GetComponent<SpriteRenderer>().bounds.size.y / 2) {
				float x2 = cosine * x1 + sine * y1;
				float vx1 = cosine * ballObj.GetComponent<Ball>().vx + sine * ballObj.GetComponent<Ball>().vy;
				float vy1 = cosine * ballObj.GetComponent<Ball>().vy - sine * ballObj.GetComponent<Ball>().vx;
				y2 = ballObj.GetComponent<SpriteRenderer>().bounds.size.y / 2;
				vy1 *= bounce;
				x1 = cosine * x2 - sine * y2;
				y1 = cosine * y2 + sine * x2;
				ballObj.GetComponent<Ball>().vx = cosine * vx1 - sine * vy1;
				ballObj.GetComponent<Ball>().vy = cosine * vy1 + sine * vx1;
				ballObj.transform.position = new Vector2(lineObj.transform.position.x + x1, lineObj.transform.position.y + y1);		
			}
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
