using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle {
	private Point3D pointA, pointB, pointC;
	private Color cl;
	public Material material;

	public Triangle(Material material, Point3D a, Point3D b, Point3D c, Color cl){
		this.material = material;
		pointA = a;
		pointB = b;
		pointC = c;
		this.cl = cl;
	}

	public void Draw() {
		material.SetPass(0);
		GL.Begin(GL.TRIANGLE_STRIP);
		GL.Color(cl);
		GL.Vertex3(pointA.ScreenX(), pointA.ScreenY(), 0);
		GL.Vertex3(pointB.ScreenX(), pointB.ScreenY(), 0);
		GL.Vertex3(pointC.ScreenX(), pointC.ScreenY(), 0);
		GL.Vertex3(pointA.ScreenX(), pointA.ScreenY(), 0);
		GL.End();
	}
}