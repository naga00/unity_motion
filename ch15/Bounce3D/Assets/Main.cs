using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float xpos, ypos, zpos;
	private float vx, vy, vz;
	private float fl = 2.5f;
	private float vpX, vpY;
	private float top = -2;
	private float bottom = 2;
	private float left = -2;
	private float right = 2;
	private float front = 2;
	private float back = -2;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.localScale = new Vector2(1, 1);
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		vpX = 0;
		vpY = 0;
		vx = Random.Range(-0.1f, 0.1f);
		vy = Random.Range(-0.1f, 0.1f);
		vz = Random.Range(-0.1f, 0.1f);
	}

	void Update() {
		xpos += vx;
		ypos += vy;
		zpos += vz;

		float radius =  ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		if(xpos + radius > right){
			xpos = right - radius;
			vx *= -1;
		}else if(xpos - radius < left){
			xpos = left + radius;
			vx *= -1;
		}
		if(ypos + radius > bottom){
			ypos = bottom - radius;
			vy *= -1;
		}else if(ypos - radius < top){
			ypos = top + radius;
			vy *= -1;
		}
		if(zpos + radius > front){
			zpos = front - radius;
			vz *= -1;
		}else if(zpos - radius < back){
			zpos = back + radius;
			vz *= -1;
		}

		if(zpos > -fl) {
			float scale = fl / (fl + zpos);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(vpX + xpos * scale, vpY + ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}
	}
}