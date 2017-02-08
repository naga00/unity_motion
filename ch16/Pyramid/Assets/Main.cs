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
		points = new Point3D[5];
		points[0] = new Point3D(  0.0f, -2.0f,  0.0f );
		points[1] = new Point3D(  2.0f,  2.0f, -2.0f );
		points[2] = new Point3D( -2.0f,  2.0f, -2.0f );
		points[3] = new Point3D( -2.0f,  2.0f,  2.0f );
		points[4] = new Point3D(  2.0f,  2.0f,  2.0f );
		for(int i=0; i<points.Length; i++) {
			points[i].SetVanishingPoint(vpX, vpY);
			points[i].SetCenter(0, 0, -2);
		}
		triangles = new Triangle[6];
		triangles[0] = new Triangle(material, points[0], points[1], points[2], GetColor(102, 102, 204, 0.5f));
		triangles[1] = new Triangle(material, points[0], points[2], points[3], GetColor(102, 204, 102, 0.5f));
		triangles[2] = new Triangle(material, points[0], points[3], points[4], GetColor(204, 102, 102, 0.5f));
		triangles[3] = new Triangle(material, points[0], points[4], points[1], GetColor(102, 204, 204, 0.5f));
		triangles[4] = new Triangle(material, points[1], points[3], points[2], GetColor(204, 102, 204, 0.5f));
		triangles[5] = new Triangle(material, points[1], points[4], points[3], GetColor(204, 102, 204, 0.5f));
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