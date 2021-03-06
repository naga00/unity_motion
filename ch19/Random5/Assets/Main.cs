﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numParticles = 300;
	private static readonly int maxRadius = 1;
	public GameObject[] particles;

	void Start () {
		QualitySettings.antiAliasing = 8;
		particles = new GameObject[numParticles];
		for(int i=0; i<numParticles; i++){
			GameObject particle = Instantiate(ballPrefab) as GameObject;
			particle.transform.localScale = new Vector2(0.08f, 0.08f);
			float x1 = Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x);
			float x2 = Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x);
			particle.transform.position = new Vector2((x1 + x2) / 2, Random.value - 0.5f);
			particle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
			particles[i] = particle;
		}
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

	private float getWorldWidth() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		return bottomRight.x;
	}

	private float getWorldHeight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		return bottomRight.y;
	}
}