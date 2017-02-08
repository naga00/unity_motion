using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle {
	private Point3D pointA, pointB, pointC;
	private uint cn;
	public Light light;
	public float zpos;
	private float red, green, blue;
	public Material material;

	public Triangle(Material material, Point3D a, Point3D b, Point3D c, uint cn){
		this.material = material;
		pointA = a;
		pointB = b;
		pointC = c;
		this.cn = cn;
	}

	public void Draw() {
		SetDepth();
		if(IsBackFace()) {
			return;
		}
		red = cn >> 16;
		green = cn >> 8 & 0xff;
		blue = cn & 0xff;
		float lightFactor = GetLightFactor();
		red *= lightFactor;
		green *= lightFactor;
		blue *= lightFactor;
		Color cl = new Color(red/255, green/255, blue/255);

		material.SetPass(0);
		GL.Begin(GL.TRIANGLE_STRIP);
		GL.Color(cl);
		GL.Vertex3(pointA.ScreenX(), pointA.ScreenY(), 0);
		GL.Vertex3(pointB.ScreenX(), pointB.ScreenY(), 0);
		GL.Vertex3(pointC.ScreenX(), pointC.ScreenY(), 0);
		GL.Vertex3(pointA.ScreenX(), pointA.ScreenY(), 0);
		GL.End();
	}

	float GetLightFactor() {
		Point3D ab = new Point3D(0, 0, 0);
		ab.x = pointA.x - pointB.x;
		ab.y = pointA.y - pointB.y;
		ab.z = pointA.z - pointB.z;

		Point3D bc = new Point3D(0, 0, 0);
		bc.x = pointB.x - pointC.x;
		bc.y = pointB.y - pointC.y;
		bc.z = pointB.z - pointC.z;

		Point3D norm = new Point3D(0, 0, 0);
		norm.x = (ab.y * bc.z) - (ab.z * bc.y);
		norm.y = -((ab.x * bc.z) - (ab.z * bc.x));
		norm.z = (ab.x * bc.y) - (ab.y * bc.x);

		float dotProd = norm.x * light.x + norm.y * light.y + norm.z * light.z;
		float normMag = Mathf.Sqrt(norm.x * norm.x + norm.y * norm.y + norm.z * norm.z);
		float lightMag = Mathf.Sqrt(light.x * light.x + light.y * light.y + light.z * light.z);

		return (Mathf.Acos(dotProd / (normMag * lightMag)) / Mathf.PI) * light.brightness;
	}

	bool IsBackFace() {
		float cax = pointC.ScreenX() - pointA.ScreenX();
		float cay= pointC.ScreenY() - pointA.ScreenY();
		float bcx = pointB.ScreenX() - pointC.ScreenX();
		float bcy = pointB.ScreenY() - pointC.ScreenY();
		return cax * bcy > cay * bcx;
	}

	void SetDepth() {
		zpos = Mathf.Min(pointA.z, pointB.z);
		zpos = Mathf.Min(zpos, pointC.z);
	}
}