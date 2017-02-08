using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numParticles = 300;
	private static readonly int maxRadius = 1;
	public GameObject[] particles;

	void Start () {
		QualitySettings.antiAliasing = 8;
		particles = new GameObject[numParticles];
		for(int i=0; i<numParticles; i++){
			GameObject particle = Instantiate(ballPrefab) as GameObject;
			particle.transform.localScale = new Vector2(0.1f, 0.1f);
			float radius = Mathf.Sqrt(Random.value) * maxRadius;
			float angle = Random.value * Mathf.PI * 2;
			particle.transform.position = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
			particle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
			particles[i] = particle;
		}
	}
}