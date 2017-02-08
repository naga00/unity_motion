using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	private Point3D[] points;
	private Triangle[] triangles;
	private static readonly int numFaces = 20;
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

		points = new Point3D[numFaces*2];
		triangles = new Triangle[numFaces*2];

		int index = 0;
		for(int i=0; i<numFaces; i++) {
			float angle = Mathf.PI * 2 / numFaces * i;
			float xpos = Mathf.Cos(angle) * 2;
			float ypos = Mathf.Sin(angle) * 2;
			points[index] = new Point3D(xpos, ypos, -1);
			points[index + 1] = new Point3D(xpos, ypos, 1);
			index += 2;
		}
		for(int i=0; i<points.Length; i++) {
			points[i].SetVanishingPoint(vpX, vpY);
			points[i].SetCenter(0, 0, 0);
		}
		index = 0;
		for(int i=0; i<numFaces-1; i++) {
			triangles[index] = new Triangle(material, points[index], points[index + 3], points[index + 1], GetColor(102, 102, 204, 0.5f));
			triangles[index + 1] = new Triangle(material, points[index], points[index + 2], points[index + 3], GetColor(102, 102, 204, 0.5f));
			index += 2;
		}
		triangles[index] = new Triangle(material, points[index], points[1], points[index + 1], GetColor(102, 102, 204, 0.5f));
		triangles[index+1] = new Triangle(material, points[index], points[0], points[1], GetColor(102, 102, 204, 0.5f));
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