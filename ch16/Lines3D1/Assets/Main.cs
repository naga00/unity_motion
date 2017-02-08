using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	public GameObject[] balls;
	private static readonly int numBalls = 50;
	private float fl = 10;
	private float vpX, vpY;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;
		this.gameObject.AddComponent<LineRenderer>();
		vpX = 0;
		vpY = 0;
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.GetComponent<Renderer>().sortingOrder = i;
			ball.transform.position = new Vector2(0, 0);
			ball.GetComponent<Ball3D>().xpos = Random.Range(-3, 3);
			ball.GetComponent<Ball3D>().ypos = Random.Range(-3, 3);
			ball.GetComponent<Ball3D>().zpos = Random.Range(-3, 3);
			ball.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
			balls[i] = ball;
		}
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angleX = (worldMousePosition.y - vpY) * 0.02f;
		float angleY = (worldMousePosition.x - vpX) * 0.02f;
		for(int i=0; i<numBalls; i++) {
			GameObject ball = balls[i];
			RotateX(ball, angleX);
			RotateY(ball, angleY);
			DoPerspective(ball);
		}
		for(int i = 1; i < numBalls; i++) {
			DrawLine(i, new Vector2(balls[i-1].transform.position.x, balls[i-1].transform.position.y), new Vector2(balls[i].transform.position.x, balls[i].transform.position.y), Color.black);
		}
	}

	private void DrawLine(int index, Vector2 v0, Vector2 v1, Color color){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.sortingOrder = -1;
		lineRenderer.material = material;
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(0.03f, 0.03f);	
		lineRenderer.SetVertexCount(numBalls);
		lineRenderer.SetPosition(index - 1, v0);
		lineRenderer.SetPosition(index, v1);
	}

	private void RotateX(GameObject ball, float angleX) {
		float cosX = Mathf.Cos(angleX);
		float sinX = Mathf.Sin(angleX);
		float y1 = ball.GetComponent<Ball3D>().ypos * cosX - ball.GetComponent<Ball3D>().zpos * sinX;
		float z1 = ball.GetComponent<Ball3D>().zpos * cosX + ball.GetComponent<Ball3D>().ypos * sinX;
		ball.GetComponent<Ball3D>().ypos = y1;
		ball.GetComponent<Ball3D>().zpos = z1;
	}

	private void RotateY(GameObject ball, float angleY) {
		float cosY = Mathf.Cos(angleY);
		float sinY = Mathf.Sin(angleY);
		float x1 = ball.GetComponent<Ball3D>().xpos * cosY - ball.GetComponent<Ball3D>().zpos * sinY;
		float z1 = ball.GetComponent<Ball3D>().zpos * cosY + ball.GetComponent<Ball3D>().xpos * sinY;
		ball.GetComponent<Ball3D>().xpos = x1;
		ball.GetComponent<Ball3D>().zpos = z1;  
	}

	private void DoPerspective(GameObject ball) {
		if(ball.GetComponent<Ball3D>().zpos > -fl) {
			float scale = fl / (fl + ball.GetComponent<Ball3D>().zpos);
			ball.transform.localScale = new Vector2(scale * 0.5f, scale * 0.5f);
			ball.transform.position = new Vector2(vpX + ball.GetComponent<Ball3D>().xpos * scale, vpY + ball.GetComponent<Ball3D>().ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}
	}
		
	private void SortZ() {
		for(int i=0; i<numBalls - 1; i++) {
			for(int j=numBalls-1; j>i; j--) {
				if(balls[j].GetComponent<Ball3D>().zpos > balls[j-1].GetComponent<Ball3D>().zpos) {
					GameObject t = balls[j] as GameObject;
					int mySortingOrder = t.GetComponent<Renderer>().sortingOrder;
					t.GetComponent<Renderer>().sortingOrder = balls[j - 1].GetComponent<Renderer>().sortingOrder;
					balls[j - 1].GetComponent<Renderer>().sortingOrder = mySortingOrder;
					balls[j] = balls[j-1];
					balls[j-1] = t;
				}
			}
		}
	}
}
