using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	private Point3D[] points;
	private Triangle[] triangles;
	private float vpX, vpY;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.white;
		Camera.main.orthographic = true;

		vpX = 0;
		vpY = 0;
		points = new Point3D[22];
		points[0] = new Point3D( -0.5f, -2.5f,  -0.5f );
		points[1] = new Point3D(  0.5f, -2.5f,  -0.5f );
		points[2] = new Point3D(  2.0f,  2.5f,  -0.5f );
		points[3] = new Point3D(  1.0f,  2.5f,  -0.5f );
		points[4] = new Point3D(  0.5f,  1.0f,  -0.5f );
		points[5] = new Point3D( -0.5f,  1.0f,  -0.5f );
		points[6] = new Point3D( -1.0f,  2.5f,  -0.5f );
		points[7] = new Point3D( -2.0f,  2.5f,  -0.5f );
		points[8] = new Point3D(  0.0f, -1.5f,  -0.5f );
		points[9] = new Point3D(  0.5f,  0.0f,  -0.5f );
		points[10] = new Point3D( -0.5f, 0.0f,  -0.5f );

		points[11] = new Point3D( -0.5f, -2.5f,  0.5f );
		points[12] = new Point3D(  0.5f, -2.5f,  0.5f );
		points[13] = new Point3D(  2.0f,  2.5f,  0.5f );
		points[14] = new Point3D(  1.0f,  2.5f,  0.5f );
		points[15] = new Point3D(  0.5f,  1.0f,  0.5f );
		points[16] = new Point3D( -0.5f,  1.0f,  0.5f );
		points[17] = new Point3D( -1.0f,  2.5f,  0.5f );
		points[18] = new Point3D( -2.0f,  2.5f,  0.5f );
		points[19] = new Point3D(  0.0f, -1.5f,  0.5f );
		points[20] = new Point3D(  0.5f,  0.0f,  0.5f );
		points[21] = new Point3D( -0.5f,  0.0f,  0.5f );

		for(int i=0; i<points.Length; i++) {
			points[i].SetVanishingPoint(vpX, vpY);
			points[i].SetCenter(0, 0, 0);
		}

		triangles = new Triangle[44];
		triangles[0] = new Triangle(material, points[0],   points[1],  points[8],  0xcccccc);
		triangles[1] = new Triangle(material, points[1],   points[9],  points[8],  0xcccccc);
		triangles[2] = new Triangle(material, points[1],   points[2],  points[9],  0xcccccc);
		triangles[3] = new Triangle(material, points[2],   points[4],  points[9],  0xcccccc);
		triangles[4] = new Triangle(material, points[2],   points[3],  points[4],  0xcccccc);
		triangles[5] = new Triangle(material, points[4],   points[5],  points[9],  0xcccccc);
		triangles[6] = new Triangle(material, points[9],   points[5],  points[10], 0xcccccc);
		triangles[7] = new Triangle(material, points[5],   points[6],  points[7],  0xcccccc);
		triangles[8] = new Triangle(material, points[5],   points[7],  points[10], 0xcccccc);
		triangles[9] = new Triangle(material, points[0],   points[10], points[7],  0xcccccc);
		triangles[10] = new Triangle(material, points[0], points[8],  points[10], 0xcccccc);

		triangles[11] = new Triangle(material, points[11], points[19], points[12], 0xcccccc);
		triangles[12] = new Triangle(material, points[12], points[19], points[20], 0xcccccc);
		triangles[13] = new Triangle(material, points[12], points[20], points[13], 0xcccccc);
		triangles[14] = new Triangle(material, points[13], points[20], points[15], 0xcccccc);
		triangles[15] = new Triangle(material, points[13], points[15], points[14], 0xcccccc);
		triangles[16] = new Triangle(material, points[15], points[20], points[16], 0xcccccc);
		triangles[17] = new Triangle(material, points[20], points[21], points[16], 0xcccccc);
		triangles[18] = new Triangle(material, points[16], points[18], points[17], 0xcccccc);
		triangles[19] = new Triangle(material, points[16], points[21], points[18], 0xcccccc);
		triangles[20] = new Triangle(material, points[11], points[18], points[21], 0xcccccc);
		triangles[21] = new Triangle(material, points[11], points[21], points[19], 0xcccccc);

		triangles[22] = new Triangle(material, points[0],  points[11], points[1],  0xcccccc);
		triangles[23] = new Triangle(material, points[11], points[12], points[1],  0xcccccc);
		triangles[24] = new Triangle(material, points[1],  points[12], points[2],  0xcccccc);
		triangles[25] = new Triangle(material, points[12], points[13], points[2],  0xcccccc);
		triangles[26] = new Triangle(material, points[3],  points[2],  points[14], 0xcccccc);
		triangles[27] = new Triangle(material, points[2],  points[13], points[14], 0xcccccc);
		triangles[28] = new Triangle(material, points[4],  points[3],  points[15], 0xcccccc);
		triangles[29] = new Triangle(material, points[3],  points[14], points[15], 0xcccccc);
		triangles[30] = new Triangle(material, points[5],  points[4],  points[16], 0xcccccc);
		triangles[31] = new Triangle(material, points[4],  points[15], points[16], 0xcccccc);
		triangles[32] = new Triangle(material, points[6],  points[5],  points[17], 0xcccccc);
		triangles[33] = new Triangle(material, points[5],  points[16], points[17], 0xcccccc);
		triangles[34] = new Triangle(material, points[7],  points[6],  points[18], 0xcccccc);
		triangles[35] = new Triangle(material, points[6],  points[17], points[18], 0xcccccc);
		triangles[36] = new Triangle(material, points[0],  points[7],  points[11], 0xcccccc);
		triangles[37] = new Triangle(material, points[7],  points[18], points[11], 0xcccccc);

		triangles[38] = new Triangle(material, points[8],  points[9],  points[19], 0xcccccc);
		triangles[39] = new Triangle(material, points[9],  points[20], points[19], 0xcccccc);
		triangles[40] = new Triangle(material, points[9],  points[10], points[20], 0xcccccc);
		triangles[41] = new Triangle(material, points[10], points[21], points[20], 0xcccccc);
		triangles[42] = new Triangle(material, points[10], points[8],  points[21], 0xcccccc);
		triangles[43] = new Triangle(material, points[8],  points[19], points[21], 0xcccccc); 

		Light light = new Light(-10, -10, -10, 1);
		for(int i=0; i<triangles.Length; i++) {
			triangles[i].light = light;
		}   
	}

	void OnPostRender() {
		Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angleX = (worldMousePosition.y - vpY) * 0.02f;
		float angleY = (worldMousePosition.x - vpX) * 0.02f;
		for(int i=0; i<points.Length; i++) {
			Point3D point = points[i];
			point.RotateX(angleX);
			point.RotateY(angleY);
		}
		SortZ();
		for(int i=0; i < triangles.Length; i++) {
			triangles[i].Draw();
		}
	}
		
	private void SortZ() {
		for(int i=0; i<triangles.Length-1; i++) {
			for(int j=triangles.Length-1; j>i; j--) {
				if(triangles[j].zpos > triangles[j-1].zpos) {
					Triangle triangle = triangles[j];
					triangles[j] = triangles[j-1];
					triangles[j-1] = triangle;
				}
			}
		}
	}
}