using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 50;
	public GameObject[] balls;
	private float fl = 15;
	private float vpX, vpY;
	private float top = -3;
	private float bottom = 3;
	private float left = -3;
	private float right = 3;
	private float front = 3;
	private float back = -3;

	void Start () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		vpX = 0;
		vpY = 0;
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.localScale = new Vector2(1, 1);
			ball.transform.position = new Vector2(0, 0);
			ball.GetComponent<Ball>().vx = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<Ball>().vz = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			balls[i] = ball;
		}
	}

	void Update() {
		for(int i=0; i<numBalls; i++){
			GameObject ball = balls[i] as GameObject;
			Move(ball);
		}
	}

	private void Move(GameObject ball) {
		float radius =  ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		ball.GetComponent<Ball>().xpos += ball.GetComponent<Ball>().vx;
		ball.GetComponent<Ball>().ypos += ball.GetComponent<Ball>().vy;
		ball.GetComponent<Ball>().zpos += ball.GetComponent<Ball>().vz;

		if(ball.GetComponent<Ball>().xpos + radius > right) {
			ball.GetComponent<Ball>().xpos = right - radius;
			ball.GetComponent<Ball>().vx *= -1;
		}else if(ball.GetComponent<Ball>().xpos - radius < left) {
			ball.GetComponent<Ball>().xpos = left + radius;
			ball.GetComponent<Ball>().vx *= -1;
		}
		if(ball.GetComponent<Ball>().ypos + radius > bottom) {
			ball.GetComponent<Ball>().ypos = bottom - radius;
			ball.GetComponent<Ball>().vy *= -1;
		}else if(ball.GetComponent<Ball>().ypos - radius < top) {
			ball.GetComponent<Ball>().ypos = top + radius;
			ball.GetComponent<Ball>().vy *= -1;
		}
		if(ball.GetComponent<Ball>().zpos + radius > front) {
			ball.GetComponent<Ball>().zpos = front - radius;
			ball.GetComponent<Ball>().vz *= -1;
		}else if(ball.GetComponent<Ball>().zpos - radius < back) {
			ball.GetComponent<Ball>().zpos = back + radius;
			ball.GetComponent<Ball>().vz *= -1;
		}

		if(ball.GetComponent<Ball>().zpos > -fl) {
			float scale = fl / (fl + ball.GetComponent<Ball>().zpos);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(vpX + ball.GetComponent<Ball>().xpos * scale, vpY + ball.GetComponent<Ball>().ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}	
	}
}