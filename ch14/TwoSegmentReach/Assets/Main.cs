using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject segment0, segment1;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		segment0 = GameObject.Find("segment0");
		segment0.transform.Translate(0, 0, 0);
		segment1 = GameObject.Find("segment1");
		segment1.transform.Translate(0, 0, 0);
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 target = Reach(segment0, worldMousePosition.x, worldMousePosition.y);
		Reach(segment1, target.x, target.y);
		segment0.transform.position = new Vector2(segment1.GetComponent<Segment>().getPin().x, segment1.GetComponent<Segment>().getPin().y);
	}
		
	private Vector2 Reach(GameObject segment, float xpos, float ypos) {
		float dx = xpos - segment.transform.position.x;
		float dy = ypos - segment.transform.position.y;
		float angle = Mathf.Atan2(dy, dx);
		segment.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
		float w = segment.GetComponent<Segment>().getPin().x - segment.transform.position.x;
		float h = segment.GetComponent<Segment>().getPin().y - segment.transform.position.y;
		float tx = xpos - w;
		float ty = ypos - h;
		return new Vector2(tx, ty);
	}
}