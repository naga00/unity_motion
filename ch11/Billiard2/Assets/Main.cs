using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball0, ball1;

	void Start () {
		ball0 = GameObject.Find("ball0") as GameObject;
		ball0.GetComponent<Ball>().mass = 2;
		ball0.GetComponent<Ball>().vx = 0.01f;
		ball0.transform.localScale = new Vector2(2, 2);
		ball0.transform.position = new Vector2(-2, 0);
		ball0.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

		ball1 = GameObject.Find("ball1") as GameObject;
		ball1.GetComponent<Ball>().mass = 1;
		ball1.GetComponent<Ball>().vx = -0.01f;
		ball1.transform.localScale = new Vector2(1, 1);
		ball1.transform.position = new Vector2(2, 0);
		ball1.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}
		
	void Update () {
		ball0.transform.Translate(ball0.GetComponent<Ball>().vx, 0, 0);
		ball1.transform.Translate(ball1.GetComponent<Ball>().vx, 0, 0);
		float dist = ball1.transform.position.x - ball0.transform.position.x;
		float ballR0 = ball0.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float ballR1 = ball1.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		if(Mathf.Abs(dist) < ballR0 + ballR1) {
			float vxTotal = ball0.GetComponent<Ball>().vx - ball1.GetComponent<Ball>().vx;
			ball0.GetComponent<Ball>().vx = ((ball0.GetComponent<Ball>().mass - ball1.GetComponent<Ball>().mass) * ball0.GetComponent<Ball>().vx + 2 * ball1.GetComponent<Ball>().mass * ball1.GetComponent<Ball>().vx) / (ball0.GetComponent<Ball>().mass + ball1.GetComponent<Ball>().mass);
			ball1.GetComponent<Ball>().vx = vxTotal + ball0.GetComponent<Ball>().vx;
			ball0.transform.Translate(ball0.GetComponent<Ball>().vx, 0, 0);
			ball1.transform.Translate(ball1.GetComponent<Ball>().vx, 0, 0);
		}
	}
}