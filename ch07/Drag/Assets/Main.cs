using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball;

	void Start () {
		ball = GameObject.Find("ball") as GameObject;
		ball.transform.position = new Vector2(0, 0);
		ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}
}