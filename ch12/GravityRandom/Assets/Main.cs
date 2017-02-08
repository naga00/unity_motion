using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject ballPrefab;
	private static readonly int numParticles = 30;
	public GameObject[] particles;

	void Start () {
		particles = new GameObject[numParticles];
		for(int i=0; i<numParticles; i++){
			GameObject particle = Instantiate(ballPrefab) as GameObject;
			float scale = Random.Range(0.2f, 1.2f);
			particle.transform.localScale = new Vector2(scale, scale);
			particle.transform.position = new Vector2(Random.Range(GetWordlTopLeft().x, GetWorldBottomRight().x), Random.Range(GetWorldBottomRight().y, GetWordlTopLeft().y));
			particle.GetComponent<Ball>().mass = scale * 0.05f;
			particle.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
			particles[i] = particle;
		}
	}
		
	void Update () {
		for(int i=0; i<numParticles; i++){
			GameObject particle = particles[i] as GameObject;
			particle.transform.Translate(particle.GetComponent<Ball>().vx, particle.GetComponent<Ball>().vy, 0, Space.World);
		}
		for(int i=0; i<numParticles; i++){
			GameObject partA = particles[i] as GameObject;
			for(int j=i+1; j<numParticles; j++){
				GameObject partB = particles[j] as GameObject;
				CheckCollision(partA, partB);
				Gravitate(partA, partB);
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

	private void CheckCollision(GameObject ball0, GameObject ball1) {
		float dx = ball1.transform.position.x - ball0.transform.position.x;
		float dy = ball1.transform.position.y - ball0.transform.position.y;
		float dist = Mathf.Sqrt(dx*dx + dy*dy);
		float ballR0 = ball0.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		float ballR1 = ball1.GetComponent<SpriteRenderer>().bounds.size.x / 2;

		if(dist < ballR0 + ballR1) {
			float angle = Mathf.Atan2(dy, dx);
			float sine = Mathf.Sin(angle);
			float cosine = Mathf.Cos(angle);
			Vector2 pos0 = new Vector2(0, 0);
			Vector2 pos1 = Rotate(dx, dy, sine, cosine, true);
			Vector2 vel0 = Rotate(ball0.GetComponent<Ball>().vx, ball0.GetComponent<Ball>().vy, sine, cosine, true);
			Vector2 vel1 = Rotate(ball1.GetComponent<Ball>().vx, ball1.GetComponent<Ball>().vy, sine, cosine, true);

			float vxTotal = vel0.x - vel1.x;
			vel0.x = ((ball0.GetComponent<Ball>().mass - ball1.GetComponent<Ball>().mass) * vel0.x + 2 * ball1.GetComponent<Ball>().mass * vel1.x) / (ball0.GetComponent<Ball>().mass + ball1.GetComponent<Ball>().mass);
			vel1.x = vxTotal + vel0.x;

			float absV = Mathf.Abs(vel0.x) + Mathf.Abs(vel1.x);
			float overlap = (ballR0 + ballR1) - Mathf.Abs(pos0.x - pos1.x);
			pos0.x += vel0.x / absV * overlap;
			pos1.x += vel1.x / absV * overlap;

			Vector2 pos0F = Rotate(pos0.x, pos0.y, sine, cosine, false);
			Vector2 pos1F = Rotate(pos1.x, pos1.y, sine, cosine, false);

			ball1.transform.position = new Vector2(ball0.transform.position.x + pos1F.x, ball0.transform.position.y + pos1F.y);
			ball0.transform.position = new Vector2(ball0.transform.position.x + pos0F.x, ball0.transform.position.y + pos0F.y);

			Vector2 vel0F = Rotate(vel0.x, vel0.y, sine, cosine, false);
			Vector2 vel1F = Rotate(vel1.x, vel1.y, sine, cosine,false);
			ball0.GetComponent<Ball>().vx = vel0F.x;
			ball0.GetComponent<Ball>().vy = vel0F.y;
			ball1.GetComponent<Ball>().vx = vel1F.x;
			ball1.GetComponent<Ball>().vy = vel1F.y;
		}
	}

	private Vector2 Rotate(float x, float y, float sine, float cosine, bool reverse) {
		Vector2 result = new Vector2(0, 0);
		if(reverse){
			result.x = x * cosine + y * sine;
			result.y = y * cosine - x * sine;
		}else{
			result.x = x * cosine - y * sine;
			result.y = y * cosine + x * sine;
		}
		return result;  
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