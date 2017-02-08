using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numParticles = 50;
	public GameObject[] particles;
	private float friction = 0.95f;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;

		particles = new GameObject[numParticles];
		for(int i=0; i<numParticles; i++){
			GameObject particle = Instantiate(ballPrefab) as GameObject;
			particle.transform.localScale = new Vector2(0.1f, 0.1f);
			particle.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			particle.GetComponent<Ball>().vx = 0;
			particle.GetComponent<Ball>().vy = 0;
			particle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
			particles[i] = particle;
		}
	}
		
	void Update () {
		for(int i=0; i<numParticles; i++){
			GameObject particle = particles[i] as GameObject;
			particle.GetComponent<Ball>().vx += Random.Range(-0.003f, 0.003f);
			particle.GetComponent<Ball>().vy += Random.Range(-0.003f, 0.003f);
			particle.transform.Translate(particle.GetComponent<Ball>().vx, particle.GetComponent<Ball>().vy, 0);
			particle.GetComponent<Ball>().vx *= friction;
			particle.GetComponent<Ball>().vy *= friction;

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

	private void Gravitate(GameObject partA, GameObject partB) {
		float dx = partB.transform.position.x - partA.transform.position.x;
		float dy = partB.transform.position.y - partA.transform.position.y;
		float distSQ = dx*dx + dy*dy;
		float dist = Mathf.Sqrt(distSQ);
		float force = partA.GetComponent<Ball>().mass * partB.GetComponent<Ball>().mass / distSQ;
		float ax = force * dx / dist;
		float ay = force * dy / dist;
		partA.GetComponent<Ball>().vx += ax;
		partA.GetComponent<Ball>().vy += ay;
		partB.GetComponent<Ball>().vx -= ax;
		partB.GetComponent<Ball>().vy -= ay;
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