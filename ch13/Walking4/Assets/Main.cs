using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject segment0, segment1, segment2, segment3;
	private float cycle;
	private float offset = - Mathf.PI / 2;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		segment0 = GameObject.Find("segment0");
		segment0.transform.Translate(-4, 2, 0);
		segment1 = GameObject.Find("segment1");
		segment1.transform.position = new Vector2(segment0.GetComponent<Segment>().getPin().x, segment0.GetComponent<Segment>().getPin().y);
	
		segment2 = GameObject.Find("segment2");
		segment2.transform.Translate(-4, 2, 0);
		segment3 = GameObject.Find("segment3");
		segment3.transform.position = new Vector2(segment2.GetComponent<Segment>().getPin().x, segment2.GetComponent<Segment>().getPin().y);	
	}

	void Update () {
		Walk(segment0, segment1, cycle);
		Walk(segment2, segment3, cycle + Mathf.PI);
		cycle += 0.03f;
	}

	void Walk(GameObject segA, GameObject segB, float cyc) {
		float angle0 = Mathf.Sin(cyc) * 45 - 90;
		float angle1 = Mathf.Sin(cyc + offset) * 45 - 45;
		segA.transform.rotation = Quaternion.Euler(0, 0, angle0);
		segB.transform.rotation = Quaternion.Euler(0, 0, segA.transform.eulerAngles.z + angle1);
		segB.transform.position = new Vector2(segA.GetComponent<Segment>().getPin().x, segA.GetComponent<Segment>().getPin().y);
	}
}
