using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float xpos, ypos, zpos;
	private float fl = 250;
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
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		xpos = worldMousePosition.x - vpX;
		ypos = worldMousePosition.y - vpY;
		float scale = fl / (fl + zpos);
		ball.transform.localScale = new Vector2(scale, scale);
		ball.transform.position = new Vector2(vpX + xpos * scale, vpY + ypos * scale);

		if (Input.GetKey("up")) {
			zpos += 1f;
		}

		if (Input.GetKey("down")) {
			zpos -= 1f;
		}
	}
}