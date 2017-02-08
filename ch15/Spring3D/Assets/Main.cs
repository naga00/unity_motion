using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float tx, ty, tz;
	private float spring = 0.1f;
	private float friction = 0.94f;
	private float fl = 5;
	private float vpX, vpY;

	void Start () {
		QualitySettings.antiAliasing = 8;
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.localScale = new Vector2(1, 1);
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		vpX = 0;
		vpY = 0;
		tx = Random.Range(-3, 3);
		ty = Random.Range(-3, 3);
		tz = Random.Range(0, 6);
	}

	private void Update() {
		float dx = tx - ball.GetComponent<Ball>().xpos;
		float dy = ty - ball.GetComponent<Ball>().ypos;
		float dz = tz - ball.GetComponent<Ball>().zpos;
		ball.GetComponent<Ball>().vx += dx * spring;
		ball.GetComponent<Ball>().vy += dy * spring;
		ball.GetComponent<Ball>().vz += dz * spring;
		ball.GetComponent<Ball>().xpos += ball.GetComponent<Ball>().vx;
		ball.GetComponent<Ball>().ypos += ball.GetComponent<Ball>().vy;
		ball.GetComponent<Ball>().zpos += ball.GetComponent<Ball>().vz;
		ball.GetComponent<Ball>().vx *= friction;
		ball.GetComponent<Ball>().vy *= friction;
		ball.GetComponent<Ball>().vz *= friction;
		if(ball.GetComponent<Ball>().zpos > -fl) {
			float scale = fl / (fl + ball.GetComponent<Ball>().zpos);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(vpX + ball.GetComponent<Ball>().xpos * scale, vpY + ball.GetComponent<Ball>().ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}	
	}

	private void OnMouseDown(){
		tx = Random.Range(-3, 3);
		ty = Random.Range(-3, 3);
		tz = Random.Range(0, 6);
	}
}