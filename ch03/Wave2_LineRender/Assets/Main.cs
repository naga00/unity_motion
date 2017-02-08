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

		this.gameObject.AddComponent<LineRenderer>();
		centerY = 0;
		xpos = GetWordlTopLeft().x;
		pxpos = xpos;
		ypos = centerY;
		pypos = ypos;
	}

	void Update () {
		xpos += xspeed;
		angle += yspeed;
		ypos = centerY - Mathf.Sin(angle) * range;
		DrawLine(new Vector2(pxpos, pypos), new Vector2(xpos, ypos));
		pxpos = xpos;
		pypos = ypos;
	}

	void OnPostRender() {
		DrawBackground(Color.white);
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

	private void DrawLine(Vector2 v0, Vector2 v1){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.material = material;
		lineRenderer.SetColors(Color.black, Color.black);
		lineRenderer.SetWidth(0.01f, 0.01f);	
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, v0);
		lineRenderer.SetPosition(1, v1);	
	}

	private Vector3 GetWordlTopLeft() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topLeft.Scale(new Vector3(1, -1, 1));
		return topLeft;
	}

	private Vector3 GetWorldBottomRight() {
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));	
		bottomRight.Scale(new Vector3(1, -1, 1));
		return bottomRight;
	}
}