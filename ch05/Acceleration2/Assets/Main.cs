using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;
	private float vx, ax;

	void Start () {
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(0, 0);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			ax = -0.004f;
		}else if(Input.GetKeyDown(KeyCode.RightArrow)){
			ax = 0.004f;
		}
		vx += ax;
		ball.transform.Translate(vx, 0, 0);
	}
}
