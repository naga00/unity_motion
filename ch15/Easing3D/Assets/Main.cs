using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float tx, ty, tz;
	private float easing = 0.05f;
	private float fl = 5;
	private float vpX, vpY;

	void Start () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;

		ball = GameObject.Find("ball") as GameObject;
		ball.transform.localScale = new Vector2(1, 1);
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		vpX = 0;
		vpY = 0;
		tx = Random.Range(-5, 5);
		ty = Random.Range(-5, 5);
		tz = Random.Range(0, 10);
	}

	void Update() {
		float dx = tx - ball.GetComponent<Ball>().xpos;
		float dy = ty - ball.GetComponent<Ball>().ypos;
		float dz = tz - ball.GetComponent<Ball>().zpos;
		ball.GetComponent<Ball>().xpos += dx * easing;
		ball.GetComponent<Ball>().ypos += dy * easing;
		ball.GetComponent<Ball>().zpos += dz * easing;

		float dist = Mathf.Sqrt(dx*dx + dy*dy + dz*dz);
		if(dist < 1) {
			tx = Random.Range(-5, 5);
			ty = Random.Range(-5, 5);
			tz = Random.Range(0, 10);
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