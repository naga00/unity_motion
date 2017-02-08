using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 50;
	public GameObject[] balls;
	private float fl = 15;
	private float vpX, vpY;

	void Start () {
		QualitySettings.antiAliasing = 8;
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.GetComponent<Renderer>().sortingOrder = i;
			ball.transform.localScale = new Vector2(1, 1);
			ball.GetComponent<Ball>().xpos = Random.Range(-3, 3);
			ball.GetComponent<Ball>().ypos = Random.Range(-3, 3);
			ball.GetComponent<Ball>().zpos = Random.Range(-3, 3);
			ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			balls[i] = ball;
		}
	}

	void Update() {
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angleX = (mouseWorldPosition.y - vpY) * 0.05f;
		float angleY = (mouseWorldPosition.x - vpX) * 0.05f;
		for(int i=0; i<numBalls; i++){
			GameObject ball = balls[i] as GameObject;
			RotateX(ball, angleX);
			RotateY(ball, angleY);
			DoPerspective(ball);
		}
		SortZ();
	}

	private void RotateX(GameObject ball, float angleX) {
		float cosX = Mathf.Cos(angleX);
		float sinX = Mathf.Sin(angleX);
		float y1 = ball.GetComponent<Ball>().ypos * cosX - ball.GetComponent<Ball>().zpos * sinX;
		float z1 = ball.GetComponent<Ball>().zpos * cosX + ball.GetComponent<Ball>().ypos * sinX;
		ball.GetComponent<Ball>().ypos = y1;
		ball.GetComponent<Ball>().zpos = z1;
	}

	private void RotateY(GameObject ball, float angleY) {
		float cosY = Mathf.Cos(angleY);
		float sinY = Mathf.Sin(angleY);
		float x1 = ball.GetComponent<Ball>().xpos * cosY - ball.GetComponent<Ball>().zpos * sinY;
		float z1 = ball.GetComponent<Ball>().zpos * cosY + ball.GetComponent<Ball>().xpos * sinY;
		ball.GetComponent<Ball>().xpos = x1;
		ball.GetComponent<Ball>().zpos = z1;
	}

	private void DoPerspective(GameObject ball) {
		if(ball.GetComponent<Ball>().zpos > -fl) {
			float scale = fl / (fl + ball.GetComponent<Ball>().zpos);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(vpX + ball.GetComponent<Ball>().xpos * scale, vpY + ball.GetComponent<Ball>().ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}		
	}

	private void SortZ() {
		for(int i=0; i<numBalls - 1; i++) {
			for(int j=numBalls-1; j>i; j--) {
				if(balls[j].GetComponent<Ball>().zpos > balls[j-1].GetComponent<Ball>().zpos) {
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