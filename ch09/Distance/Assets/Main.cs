using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ball1, ball2;

	void Start () {
		ball1 = GameObject.Find("ball1") as GameObject;
		ball1.transform.localScale = new Vector2(2, 2);
		ball1.transform.position = new Vector2(0, 0);
		ball1.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

		ball2 = GameObject.Find("ball2") as GameObject;
		ball2.transform.localScale = new Vector2(2, 2);
		ball2.transform.position = new Vector2(GetWordlTopLeft().x, GetWordlTopLeft().y);
		ball2.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	void Update () {
		Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		ball2.transform.position = new Vector2(worldMousePosition.x, worldMousePosition.y);
		float dx = ball2.transform.position.x - ball1.transform.position.x;
		float dy = ball2.transform.position.y - ball1.transform.position.y;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);
		if(dist < 2) {
			Debug.Log("hit");
		}
	}

	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1, -1, 1));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1, -1, 1));
		return bottomRight;
	}
}