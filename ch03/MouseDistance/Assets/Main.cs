using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	public GameObject ball;
	public GameObject textField;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		ball = GameObject.Find("ball");
		ball.transform.position = new Vector2(0, 0);
		textField = GameObject.Find("textField");
	}

	void Update () {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		DrawLine(new Vector2(ball.transform.position.x, ball.transform.position.y), new Vector2(worldMousePosition.x, worldMousePosition.y));
		float dx = ball.transform.position.x - worldMousePosition.x;
		float dy = ball.transform.position.y - worldMousePosition.y;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);
		textField.GetComponent<Text>().text = dist.ToString();
	}

	private void DrawLine(Vector2 v0, Vector2 v1){
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.enabled = true;
		lineRenderer.material = material;
		lineRenderer.SetColors(new Color(0, 0, 0, 1), new Color(0, 0, 0, 1));
		lineRenderer.SetWidth(0.02f, 0.02f);	
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, v0);
		lineRenderer.SetPosition(1, v1);	
	}
}