using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject ship;
	private float vr;
	private float thrust;
	private float vx, vy;

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
			thrust = 0.001f;
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
		ship.transform.Translate(vx, vy, 0, Space.World);
		ship.GetComponent<Ship>().PostRender();
	}
}