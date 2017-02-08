using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	private float angle;
	private float centerY;
	private float range = 1.5f;
	private float xspeed = 0.02f;
	private float yspeed = 0.05f;
	private float xpos, ypos;
	private float pxpos, pypos;
	public Material material;
	private bool isDrawBackground;

	void Start () {
		QualitySettings.antiAliasing = 8;
		Camera.main.clearFlags = CameraClearFlags.Nothing;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		centerY = 0;
		xpos = GetWordlTopLeft().x;
		pxpos = xpos;
		ypos = centerY;
		pypos = ypos;
	}
		
	void OnPostRender() {
		DrawBackground(Color.white);
		xpos += xspeed;
		angle += yspeed;
		ypos = centerY - Mathf.Sin(angle) * range;
		DrawLine(new Vector3(pxpos, pypos, 0), new Vector3(xpos, ypos, 0), Color.black);
		pxpos = xpos;
		pypos = ypos;
	}

	private void DrawBackground(Color color) {
		if (! isDrawBackground) {
			material.SetPass(0);
			GL.Begin(GL.QUADS);
			GL.Color(color);
			GL.Vertex3(GetWordlTopLeft().x, GetWordlTopLeft().y, 0);
			GL.Vertex3(GetWorldBottomRight().x, GetWordlTopLeft().y, 0);
			GL.Vertex3(GetWorldBottomRight().x, GetWorldBottomRight().y, 0);
			GL.Vertex3(GetWordlTopLeft().x, GetWorldBottomRight().y, 0);
			GL.End();
			isDrawBackground = true;
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
		
	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1f, -1f, 1f));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1f, -1f, 1f));
		return bottomRight;
	}
}