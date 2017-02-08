using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject segment0, segment1;
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
	}

	void Update () {
		cycle += 0.03f;
		float angle0 = Mathf.Sin(cycle) * 45 - 90;
		float angle1 = Mathf.Sin(cycle + offset) * 45 - 45;
		segment0.transform.rotation = Quaternion.Euler(0, 0, angle0);
		segment1.transform.rotation = Quaternion.Euler(0, 0, segment0.transform.eulerAngles.z + angle1);
		segment1.transform.position = new Vector2(segment0.GetComponent<Segment>().getPin().x, segment0.GetComponent<Segment>().getPin().y);
	}
}