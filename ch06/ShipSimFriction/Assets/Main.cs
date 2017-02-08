using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject ship;
	private float vr;
	private float thrust;
	private float vx, vy;
	private float friction = 0.97f;

	void Start () {
		QualitySettings.antiAliasing = 8;
		ship = GameObject.Find("Ship") as GameObject;
		ship.transform.position = new Vector2(0, 0);
	}

	void OnPostRender() {
		if (Input.GetKey("left")) {
			vr = 5;
		} else if (Input.GetKey("right")) {
			vr = -5;
		} else if (Input.GetKey("up")) {
			thrust = 0.005f;
			ship.GetComponent<Ship>().gun(true);
		} else {
			vr = 0;
			thrust = 0;
			ship.GetComponent<Ship>().gun(false);  
		}

		ship.transform.Rotate(0, 0, vr, Space.World);
		float angle = ship.transform.eulerAngles.z * Mathf.PI / 180;
		float ax = Mathf.Cos(angle) * thrust;
		float ay = Mathf.Sin(angle) * thrust;
		vx += ax;
		vy += ay;
		vx *= friction;
		vy *= friction;
		ship.transform.Translate(vx, vy, 0, Space.World);

		float shipW = ship.GetComponent<Ship>().width;
		float shipH = ship.GetComponent<Ship>().height;
		float left = GetWordlTopLeft().x;
		float right = GetWorldBottomRight().x;
		float top = GetWordlTopLeft().y;
		float bottom = GetWorldBottomRight().y;
	
		if(ship.transform.position.x - shipW / 2 > right){
			ship.transform.position = new Vector2(left - shipW / 2, ship.transform.position.y);
		}else if (ship.transform.position.x + shipW / 2 < left){
			ship.transform.position = new Vector2(right + shipW / 2, ship.transform.position.y);
		}

		if(ship.transform.position.y + shipH / 2 < bottom){
			ship.transform.position = new Vector2(ship.transform.position.x, top + shipH / 2);
		}else if (ship.transform.position.y - shipH / 2 > top ){
			ship.transform.position = new Vector2(ship.transform.position.x, bottom - shipH / 2);
		}  
	
		ship.GetComponent<Ship>().PostRender();
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