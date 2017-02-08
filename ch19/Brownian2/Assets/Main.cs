using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	private static readonly int numDots = 20;
	public Ball[] dots;
	private float friction = 0.95f;
	public Material material;
	private bool isDrawBackground;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		Camera.main.clearFlags = CameraClearFlags.Nothing;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;
		dots = new Ball[numDots];
		for(int i=0; i<numDots; i++){
			dots[i] = new Ball();
			Ball particle = dots[i];
			particle.x = Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x);
			particle.y = Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y);
			particle.vx = 0;
			particle.vy = 0;
		}
	}
		
	void OnPostRender() {
		DrawBackground(Color.white);
		for (int i = 0; i < numDots; i++) {
			Ball dot = dots[i];
			dot.vx += Random.Range(-0.003f, 0.003f);
			dot.vy += Random.Range(-0.003f, 0.003f);
			dot.x += dot.vx;
			dot.y += dot.vy;
			dot.vx *= friction;
			dot.vy *= friction;
			if(dot.x > GetWorldBottomRight().x){
				dot.x = GetWordlTopLeft().x;
			}else if(dot.x < GetWordlTopLeft().x){
				dot.x = GetWorldBottomRight().x;
			}
			if(dot.y > GetWordlTopLeft().y){
				dot.y = GetWorldBottomRight().y;
			}else if(dot.y < GetWorldBottomRight().y){
				dot.y = GetWordlTopLeft().y;
			}
			DrawRect(dot.x, dot.y, 0.04f, 0.04f, Color.black);
		}
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

	private void DrawRect(float x, float y, float width, float height, Color color) {
		material.SetPass(0);
		GL.Begin(GL.QUADS);
		GL.Color(color);
		GL.Vertex3(x, y, 0);
		GL.Vertex3(x + width, y, 0);
		GL.Vertex3(x + width, y + height, 0);
		GL.Vertex3(x, y + height, 0);
		GL.End();
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