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
		this.gameObject.AddComponent<LineRenderer>();

		vpX = 0;
		vpY = 0;
		points = new Point3D[numPoints];
		points[0] = new Point3D(-3.0f, -3.0f,  0.0f);
		points[1] = new Point3D( 3.0f, -3.0f,  0.0f);
		points[2] = new Point3D( 3.0f,  3.0f,  0.0f);
		points[3] = new Point3D(-3.0f,  3.0f,  0.0f);
		points[4] = new Point3D(-3.0f, -3.0f,  0.0f);
		for(int i=0; i<numPoints; i++) {
			points[i].SetVanishingPoint(vpX, vpY);
		}
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angleX = (worldMousePosition.y - vpY) * 0.02f;
		float angleY = (worldMousePosition.x - vpX) * 0.02f;
		for(int i=0; i<numPoints; i++) {
			Point3D point = points[i];
			point.RotateX(angleX);
			point.RotateY(angleY);
		}
		for(int i = 1; i < numPoints; i++) {
			DrawLine(i, new Vector2(points[i-1].ScreenX(), points[i-1].ScreenY()), new Vector2(points[i].ScreenX(), points[i].ScreenY()), Color.black);
		}
	}

	private void DrawLine(int index, Vector2 v0, Vector2 v1, Color color){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.sortingOrder = -1;
		lineRenderer.material = material;
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(0.03f, 0.03f);	
		lineRenderer.SetVertexCount(numPoints);
		lineRenderer.SetPosition(index - 1, v0);
		lineRenderer.SetPosition(index, v1);
	}
}