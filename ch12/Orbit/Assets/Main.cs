using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private ArrayList particles;
	static readonly int numParticles = 2;

	void Start () {
		QualitySettings.antiAliasing = 8;
		particles = new ArrayList();

		GameObject sun = Instantiate(ballPrefab) as GameObject;
		sun.transform.localScale = new Vector2(2.5f, 2.5f);
		sun.transform.position = new Vector2(0, 0);
		sun.GetComponent<Ball>().vx = 0;
		sun.GetComponent<Ball>().vy = 0;
		sun.GetComponent<Ball>().mass = 1;
		sun.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
		particles.Add(sun);

		GameObject planet = Instantiate(ballPrefab) as GameObject;
		planet.transform.localScale = new Vector2(0.4f, 0.4f);
		planet.transform.position = new Vector2(4.5f, 0.0f);
		planet.GetComponent<Ball>().vx = 0;
		planet.GetComponent<Ball>().vy = -0.07f;
		planet.GetComponent<Ball>().mass = 0.0001f;
		planet.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
		particles.Add(planet);
	}
		
	void Update () {
		for(int i=0; i<numParticles; i++){
			GameObject particle = particles[i] as GameObject;
			particle.transform.Translate(particle.GetComponent<Ball>().vx, particle.GetComponent<Ball>().vy, 0, Space.World);
		}
		for(int i=0; i<numParticles - 1; i++){
			GameObject partA = particles[i] as GameObject;
			for(int j=i+1; j<numParticles; j++){
				GameObject partB = particles[j] as GameObject;
				Gravitate(partA, partB);
			}
		}
	}
		
	private void Gravitate(GameObject partA, GameObject partB) {
		float dx = partB.transform.position.x - partA.transform.position.x;
		float dy = partB.transform.position.y - partA.transform.position.y;
		float distSQ = dx*dx + dy*dy;
		float dist = Mathf.Sqrt(distSQ);
		float force = (partA.GetComponent<Ball>().mass * partB.GetComponent<Ball>().mass) / distSQ;
		float ax = force * dx * dist / 1000;
		float ay = force * dy * dist / 1000;
		partA.GetComponent<Ball>().vx += ax / partA.GetComponent<Ball>().mass;
		partA.GetComponent<Ball>().vy += ay / partA.GetComponent<Ball>().mass;
		partB.GetComponent<Ball>().vx -= ax / partB.GetComponent<Ball>().mass;
		partB.GetComponent<Ball>().vy -= ay / partB.GetComponent<Ball>().mass;
	}
}