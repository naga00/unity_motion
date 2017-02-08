using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject arrow;
	private float vr = -5;

	void Start () {
		arrow = GameObject.Find("arrow");
		arrow.transform.position = new Vector2(0, 0);
	}

	void Update () {
		arrow.transform.Rotate(new Vector3(0, 0, vr));
	}
}