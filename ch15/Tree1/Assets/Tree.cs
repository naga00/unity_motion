using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree {
	public float x, y;
	public float xpos, ypos, zpos;
	public float scaleX, scaleY;
	private float x0, y0, x1, y1, x2, y2, x3, y3, x4, y4;
	public Color color;
	public Material material;

	public Tree(Material material){
		this.material = material;
		x0 = 0;
		y0 = 3.5f + Random.value * 0.5f;
		x1 = 0;
		y1 = 0.7f + Random.value * 0.7f;
		x2 = Random.value * 4.0f - 2.0f;
		y2 = 2.0f + Random.value * 1.0f;
		x3 = 0;
		y3 = 1.5f + Random.value * 1.0f;
		x4 = Random.value * 3.0f - 1.5f;
		y4 = 2.5f + Random.value * 0.0f;
	}


	public void OnDraw () {
		DrawLine(x + 0, y + 0, x + x0, y + y0, color);
		DrawLine(x + x1, y + y1, x + x2, y + y2, color);
		DrawLine(x + x3, y + y3, x + x4, y + y4, color);
	}

	private void DrawLine(float x0, float y0, float x1, float y1, Color color){
		material.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(color);
		GL.Vertex3(x0 * scaleX, y0* scaleY, 0);
		GL.Vertex3(x1* scaleX, y1* scaleY, 0);
		GL.End();
	}
}