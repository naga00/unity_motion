using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float xpos, ypos, zpos;
	private float vx, vy, vz;
	private float friction = 0.98f;
	private float fl = 25;
	private float vpX, vpY;

	void Start () {
		QualitySettings.antiAliasing = 8;
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.localScale = new Vector2(1, 1);
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		vpX = 0;
		vpY = 0;
	}

	void Update() {
		xpos += vx;
		ypos += vy;
		zpos += vz;

		vx *= friction;
		vy *= friction;
		vz *= friction;

		if(zpos > -fl) {
			float scale = fl / (fl + zpos);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(vpX + xpos * scale, vpY + ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			vy += 0.01f;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			vy -= 0.01f;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			vx -= 0.01f;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			vx += 0.01f;
		}

		if (Input.GetKey(KeyCode.LeftAlt)) {
			vz += 0.01f;
		}

		if (Input.GetKey(KeyCode.RightAlt)) {
			vz += 0.01f;
		}

		if (Input.GetKey(KeyCode.LeftControl)) {
			vz -= 0.01f;
		}

		if (Input.GetKey(KeyCode.RightControl)) {
			vz -= 0.01f;
		}
	}
}