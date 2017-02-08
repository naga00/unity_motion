using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	public Material material;
	public bool isGun;
	public float width = 0.6f;
	public float height = 0.5f;

	void Start () {
		isGun = false;
	}
		
	public void gun(bool isGun) {
		this.isGun = isGun;
	}

	public void PostRender() {
		material.SetPass(0);	
		if (isGun) {
			GL.PushMatrix();
			GL.MultMatrix (transform.localToWorldMatrix);
			GL.Begin(GL.LINES);
			GL.Color(new Color(1, 0, 0, 1));
			GL.Vertex3(-0.2f, -0.15f, 0);
			GL.Vertex3(-0.35f, 0, 0);
			GL.Vertex3(-0.35f, 0, 0);
			GL.Vertex3(-0.2f, 0.15f, 0);
			GL.End();
			GL.PopMatrix();
		}
		GL.PushMatrix();
		GL.MultMatrix (transform.localToWorldMatrix);
		GL.Begin(GL.LINES);
		GL.Color(new Color(1, 0, 0, 1));
		GL.Vertex3(0.25f, 0, 0);
		GL.Vertex3(-0.25f, 0.25f, 0);
		GL.Vertex3(-0.25f, 0.25f, 0);
		GL.Vertex3(-0.15f, 0, 0);
		GL.Vertex3(-0.15f, 0, 0);
		GL.Vertex3(-0.25f, -0.25f, 0);
		GL.Vertex3(-0.25f, -0.25f, 0);
		GL.Vertex3(0.25f, 0, 0);
		GL.End();
		GL.PopMatrix();
	}
}