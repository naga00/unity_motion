using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject arrow;

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
	}
}