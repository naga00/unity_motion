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
		if(zpos > -fl) {
			Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			xpos = worldMousePosition.x - vpX;
			ypos = worldMousePosition.y - vpY;
			float scale = fl / (fl + zpos);
			ball.transform.localScale = new Vector2(scale, scale);
			ball.transform.position = new Vector2(vpX + xpos * scale, vpY + ypos * scale);
			ball.SetActive(true);
		}else{
			ball.SetActive(false);
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			zpos += 1;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			zpos -= 1;
		}
	}
}