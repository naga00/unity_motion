using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numParticles = 50;
	public GameObject[] particles;

	void Start () {
		QualitySettings.antiAliasing = 8;
		particles = new GameObject[numParticles];
		for(int i=0; i<numParticles; i++){
			GameObject particle = Instantiate(ballPrefab) as GameObject;
			particle.transform.localScale = new Vector2(0.1f, 0.1f);
			particle.transform.position = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
			particle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
			particles[i] = particle;
		}
	}
}