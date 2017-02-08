using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject segmentPrefab;
	public GameObject[] segments;
	private static readonly int numSegments = 6;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		segments = new GameObject[numSegments];
		for (int i = 0; i < numSegments; i++) {
			GameObject segment = Instantiate(segmentPrefab) as GameObject;
			segment.transform.position = new Vector2(0, 0);
			segments[i] = segment;
		}
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 target = Reach(segments[0], worldMousePosition.x, worldMousePosition.y);

		for(int i=1; i<numSegments; i++){
			GameObject segment = segments[i];
			target = Reach(segment, target.x, target.y);
		}

		for(int i = numSegments - 1; i > 0; i--){
			GameObject segmentA = segments[i];
			GameObject segmentB = segments[i - 1];
			Position(segmentB, segmentA);
		}
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

	private void Position(GameObject segmentA, GameObject segmentB) {
		segmentA.transform.position = new Vector2(segmentB.GetComponent<Segment>().getPin().x, segmentB.GetComponent<Segment>().getPin().y);
	}
}