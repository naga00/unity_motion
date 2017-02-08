using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject arrow;

	void Start () {
		arrow = GameObject.Find("arrow");
	}

	void Update () {
		arrow.transform.position = new Vector2(0, 0);
	}
}
