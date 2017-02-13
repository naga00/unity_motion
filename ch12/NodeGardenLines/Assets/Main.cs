using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numParticles = 30;
	public GameObject[] particles;
	private float minDist = 1;
	private float springAmount = 0.001f;
	public Material material;

	void Start () {
		//Screen.fullScreen = false;
		//Screen.SetResolution(1000, 1000, Screen.fullScreen);
		QualitySettings.antiAliasing = 8;
		particles = new GameObject[numParticles];
		for(int i=0; i<numParticles; i++){
			GameObject particle = Instantiate(ballPrefab) as GameObject;
			particle.transform.localScale = new Vector2(0.1f, 0.1f);
			particle.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			particle.GetComponent<Ball>().vx = Random.Range(-0.03f, 0.03f);
			particle.GetComponent<Ball>().vy = Random.Range(-0.03f, 0.03f);
			particle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
			particles[i] = particle;
		}
	}

	void OnPostRender() {
		for(int i=0; i<numParticles; i++){
			GameObject partA = particles[i] as GameObject;
			for(int j=i+1; j<numParticles; j++){
				GameObject partB = particles[j] as GameObject;
				Spring(partA, partB);
			}
		}
		for(int i=0; i<numParticles; i++){
			GameObject particle = particles[i] as GameObject;  
			particle.transform.Translate(particle.GetComponent<Ball>().vx, particle.GetComponent<Ball>().vy, 0);
			if(particle.transform.position.x > GetWorldBottomRight().x){
				particle.transform.position = new Vector2(GetWordlTopLeft().x, particle.transform.position.y);
			}else if(particle.transform.position.x < GetWordlTopLeft().x){
				particle.transform.position = new Vector2(GetWorldBottomRight().x, particle.transform.position.y);
			}
			if(particle.transform.position.y > GetWordlTopLeft().y){
				particle.transform.position = new Vector2(particle.transform.position.x, GetWorldBottomRight().y);
			}else if(particle.transform.position.y < GetWorldBottomRight().y){
				particle.transform.position = new Vector2(particle.transform.position.x, GetWordlTopLeft().y);
			}
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

	private void Spring(GameObject partA, GameObject partB) {
		float dx = partB.transform.position.x - partA.transform.position.x;
		float dy = partB.transform.position.y - partA.transform.position.y;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);
		if(dist < minDist) {
			float alpha = dist / minDist;
			DrawLine(new Vector2(partA.transform.position.x, partA.transform.position.y), new Vector2(partB.transform.position.x, partB.transform.position.y), new Color(1, 1, 1, alpha));
			float ax = dx * springAmount;
			float ay = dy * springAmount;
			partA.GetComponent<Ball>().vx += ax;
			partA.GetComponent<Ball>().vy += ay;
			partB.GetComponent<Ball>().vx -= ax;
			partB.GetComponent<Ball>().vy -= ay;
		}
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