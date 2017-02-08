using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 20;
	public GameObject[] balls;
	private float fl = 5;
	private float vpX, vpY;
	private float top = -6;
	private float bottom = 6;
	private float left = -6;
	private float right = 6;
	private float front = 6;
	private float back = -6;

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
			ball.GetComponent<Ball>().vx = Random.Range(-0.3f, 0.3f);
			ball.GetComponent<Ball>().vy = Random.Range(-0.3f, 0.3f);
			ball.GetComponent<Ball>().vz = Random.Range(-0.3f, 0.3f);
			ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			balls[i] = ball;
		}
	}

	void Update() {
		for(int i=0; i<numBalls; i++) {
			GameObject ball = balls[i] as GameObject;
			Move(ball);
		}
		for(int i=0; i<numBalls - 1; i++) {
			GameObject ballA = balls[i] as GameObject;
			float radiusA =  ballA.GetComponent<SpriteRenderer>().bounds.size.x / 2;
			for(int j=i+1; j<numBalls; j++) {
				GameObject ballB = balls[j] as GameObject;
				float dx = ballA.GetComponent<Ball>().xpos - ballB.GetComponent<Ball>().xpos;
				float dy = ballA.GetComponent<Ball>().ypos - ballB.GetComponent<Ball>().ypos;
				float dz = ballA.GetComponent<Ball>().zpos - ballB.GetComponent<Ball>().zpos;
				float dist = Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
				float radiusB =  ballB.GetComponent<SpriteRenderer>().bounds.size.x / 2;
				if(dist < radiusA + radiusB) {
					ballA.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
					ballB.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
				}
			}
		}
		SortZ();
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