using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point3D {
	public float fl = 10;
	public float vpX, vpY;
	public float cX, cY, cZ;
	public float x, y, z;

	public Point3D(float x, float y, float z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public void SetVanishingPoint(float vpX, float vpY) {
		this.vpX = vpX;
		this.vpY = vpY;
	}

	public void SetCenter(float cX, float cY, float cZ) {
		this.cX = cX;
		this.cY = cY;
		this.cZ = cZ;
	}

	public float ScreenX() {
		float scale = fl / (fl + z + cZ);
		return vpX + (cX + x) * scale;
	}

	public float ScreenY() {
		float scale = fl / (fl + z + cZ);
		return vpY + (cY + y) * scale;
	}

	public void RotateX(float angleX) {
		float cosX = Mathf.Cos(angleX);
		float sinX = Mathf.Sin(angleX);
		float y1 = y * cosX - z * sinX;
		float z1 = z * cosX + y * sinX;
		y = y1;
		z = z1;
	}

	public void RotateY(float angleY) {
		float cosY = Mathf.Cos(angleY);
		float sinY = Mathf.Sin(angleY);
		float x1 = x * cosY - z * sinY;
		float z1 = z * cosY + x * sinY;
		x = x1;
		z = z1;
	}

	public void RotateZ(float angleZ) {
		float cosZ = Mathf.Cos(angleZ);
		float sinZ = Mathf.Sin(angleZ);
		float x1 = x * cosZ - y * sinZ;
		float y1 = y * cosZ + x * sinZ;
		x = x1;
		y = y1;
	}
}
