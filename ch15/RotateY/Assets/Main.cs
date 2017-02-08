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
		float angleY = (mouseWorldPosition.y - vpY) * 0.05f;
		for(int i=0; i<numBalls; i++){
			GameObject ball = balls[i] as GameObject;
			RotateY(ball, angleY);
		}
		SortZ();
	}

	private void RotateY(GameObject ball, float angleY) {
		float cosY = Mathf.Cos(angleY);
		float sinY = Mathf.Sin(angleY);
		float x1 = ball.GetComponent<Ball>().xpos * cosY + ball.GetComponent<Ball>().zpos * sinY;
		float z1 = ball.GetComponent<Ball>().zpos * cosY - ball.GetComponent<Ball>().xpos * sinY;
		ball.GetComponent<Ball>().xpos = x1;
		ball.GetComponent<Ball>().zpos = z1;

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
		for(int i=0; i<numBalls-1; i++) {
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