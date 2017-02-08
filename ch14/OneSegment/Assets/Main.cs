using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject segment0;
	private float cycle;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		segment0 = GameObject.Find("segment0");
		segment0.transform.Translate(0, 0, 0);
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float dx = worldMousePosition.x - segment0.transform.position.x;
		float dy = worldMousePosition.y - segment0.transform.position.y;
		float angle = Mathf.Atan2(dy, dx);
		segment0.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
	}
}