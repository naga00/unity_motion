using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	private Point3D[] points;
	private Triangle[] triangles;
	private float vpX, vpY;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		vpX = 0;
		vpY = 0;
		points = new Point3D[11];
		points[0] = new Point3D( -0.5f, -2.5f, 1);
		points[1] = new Point3D(  0.5f, -2.5f, 1);
		points[2] = new Point3D(  2.0f,  2.5f, 1);
		points[3] = new Point3D(  1.0f,  2.5f, 1);
		points[4] = new Point3D(  0.5f,  1.0f, 1);
		points[5] = new Point3D( -0.5f,  1.0f, 1);
		points[6] = new Point3D( -1.0f,  2.5f, 1.0f);
		points[7] = new Point3D( -2.0f,  2.5f, 1.0f);
		points[8] = new Point3D(  0.0f, -1.5f, 1.0f);
		points[9] = new Point3D(  0.5f,  0.0f, 1.0f);
		points[10] = new Point3D(-0.5f,  0.0f, 1.0f);
		for(int i=0; i<points.Length; i++) {
			points[i].SetVanishingPoint(vpX, vpY);
			points[i].SetCenter(0.0f, 0.0f, 0.0f);
		}
		triangles = new Triangle[11];
		triangles[0] =  new Triangle(material, points[0], points[1], points[8], GetColor(255, 204, 204, 1));
		triangles[1] =  new Triangle(material, points[1], points[9], points[8], GetColor(255, 204, 204, 1));
		triangles[2] =  new Triangle(material, points[1], points[2], points[9], GetColor(255, 204, 204, 1));
		triangles[3] =  new Triangle(material, points[2], points[4], points[9], GetColor(255, 204, 204, 1));
		triangles[4] =  new Triangle(material, points[2], points[3], points[4], GetColor(255, 204, 204, 1));
		triangles[5] =  new Triangle(material, points[4], points[5], points[9], GetColor(255, 204, 204, 1));
		triangles[6] =  new Triangle(material, points[9], points[5], points[10], GetColor(255, 204, 204, 1));
		triangles[7] =  new Triangle(material, points[5], points[6], points[7], GetColor(255, 204, 204, 1));
		triangles[8] =  new Triangle(material, points[5], points[7], points[10], GetColor(255, 204, 204, 1));
		triangles[9] =  new Triangle(material, points[0], points[10],points[7], GetColor(255, 204, 204, 1));
		triangles[10] = new Triangle(material, points[0], points[8], points[10], GetColor(255, 204, 204, 1));
	}

	void OnPostRender() {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angleX = (worldMousePosition.y - vpY) * 0.02f;
		float angleY = (worldMousePosition.x - vpX) * 0.02f;
		for(int i=0; i<points.Length; i++) {
			Point3D point = points[i];
			point.RotateX(angleX);
			point.RotateY(angleY);
		}
		for(int i=0; i < triangles.Length; i++) {
			triangles[i].Draw();
		}
	}

	private Color GetColor(float r, float g, float b, float a) {
		return new Color(r/255, g/255, b/255, a);
	}
}