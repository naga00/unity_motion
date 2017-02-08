using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	private Point3D[] points;
	private static readonly int numPoints = 5;
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
		points = new Point3D[numPoints];
		points[0] = new Point3D( -3, -3,  0 );
		points[1] = new Point3D(  3, -3,  0 );
		points[2] = new Point3D(  3,  3,  0 );
		points[3] = new Point3D( -3,  3,  0 );
		points[4] = new Point3D( -3, -3,  0 );
		for(int i=0; i<numPoints; i++) {
			points[i].SetVanishingPoint(vpX, vpY);
		}
	}

	void OnPostRender() {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angleX = (worldMousePosition.y - vpY) * 0.02f;
		float angleY = (worldMousePosition.x - vpX) * 0.02f;
		for(int i=0; i<numPoints; i++) {
			Point3D point = points[i];
			point.RotateX(angleX);
			point.RotateY(angleY);
		}

		for(int i = 1; i < numPoints; i++) {
			DrawLine(new Vector2(points[i-1].ScreenX(), points[i-1].ScreenY()), new Vector2(points[i].ScreenX(), points[i].ScreenY()), Color.black);
		}
	}
		
	private void DrawLine(Vector3 v0, Vector3 v1, Color color) {
		material.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(color);
		GL.Vertex3(v0.x, v0.y, v0.z);
		GL.Vertex3(v1.x, v1.y, v1.z);
		GL.End();
	}
}