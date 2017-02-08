using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject arrow;
	private float vx, vy;
	private float force = 0.1f;

	void Start () {
		arrow = GameObject.Find("arrow");
		arrow.transform.position = new Vector2(0, 0);
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float dx = worldMousePosition.x - arrow.transform.position.x;
		float dy = worldMousePosition.y - arrow.transform.position.y;
		float angle = Mathf.Atan2(dy, dx);
		arrow.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);

		float ax = Mathf.Cos(angle) * force;
		float ay = Mathf.Sin(angle) * force;
		vx += ax;
		vy += ay;
		arrow.transform.position = new Vector2(arrow.transform.position.x + vx, arrow.transform.position.y + vy);
	}
}
