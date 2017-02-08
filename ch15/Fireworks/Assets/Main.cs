using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numBalls = 100;
	public GameObject[] balls;
	private float fl = 15;
	private float vpX, vpY;
	private float gravity = -0.002f;
	private float floor = -2;
	private float bounce = -0.3f;

	void Start () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		QualitySettings.antiAliasing = 8;
		vpX = 0;
		vpY = 0;
		balls = new GameObject[numBalls];
		for (int i = 0; i < numBalls; i++) {
			GameObject ball = Instantiate(ballPrefab) as GameObject;
			ball.transform.localScale = new Vector2(0.1f, 0.1f);
			ball.transform.position = new Vector2(0, 0);
			ball.GetComponent<Ball>().ypos = 2;
			ball.GetComponent<Ball>().vx = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<Ball>().vy = Random.Range(0.0f, 0.2f);
			ball.GetComponent<Ball>().vz = Random.Range(-0.1f, 0.1f);
			ball.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);
			balls[i] = ball;
		}
	}

	void Update() {
		for(int i=0; i<numBalls; i++){
			GameObject ball = balls[i] as GameObject;
			Move(ball);
		}
		SortZ();
	}

	private void Move(GameObject ball) {
		ball.GetComponent<Ball>().vy += gravity;
		ball.GetComponent<Ball>().xpos += ball.GetComponent<Ball>().vx;
		ball.GetComponent<Ball>().ypos += ball.GetComponent<Ball>().vy;
		ball.GetComponent<Ball>().zpos += ball.GetComponent<Ball>().vz;

		if(ball.GetComponent<Ball>().ypos < floor) {
			ball.GetComponent<Ball>().ypos = floor;
			ball.GetComponent<Ball>().vy *= bounce;
		}
	
		if(ball.GetComponent<Ball>().zpos > -fl) {
			float scale = fl / (fl + ball.GetComponent<Ball>().zpos);
			ball.transform.localScale = new Vector2(scale  * 0.1f, scale  * 0.1f);
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
					balls[j] = balls[j-1];
					balls[j-1] = t;
				}
			}
		}
	}
}