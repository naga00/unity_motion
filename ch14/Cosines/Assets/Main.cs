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

		float dx = worldMousePosition.x - segment1.transform.position.x;
		float dy = worldMousePosition.y - segment1.transform.position.y;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);

		float a = 1;
		float b = 1;
		float c = Mathf.Min(dist, a + b);

		float B = Mathf.Acos((b * b - a * a - c * c) / (-2 * a * c));
		float C = Mathf.Acos((c * c - a * a - b * b) / (-2 * a * b));
		float D = Mathf.Atan2(dy, dx);
		float E = D + B + Mathf.PI + C;

		segment1.transform.rotation = Quaternion.Euler(0, 0, (D + B) * 180 / Mathf.PI);
		segment0.transform.position = new Vector2(segment1.GetComponent<Segment>().getPin().x, segment1.GetComponent<Segment>().getPin().y);
		segment0.transform.rotation = Quaternion.Euler(0, 0, E * 180 / Mathf.PI);
	}
}