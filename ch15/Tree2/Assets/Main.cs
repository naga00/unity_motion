using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	private Tree[] trees;
	private static readonly int numTrees = 100;
	private float fl = 25;
	private float vpX, vpY;
	private float floor = -1;
	private float ax, ay, az;
	private float vx, vy, vz;
	private float gravity = -0.3f;
	private float friction = 0.98f;
	public Material material;

	void Start () {
		QualitySettings.antiAliasing = 8;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.black;
		Camera.main.orthographic = true;
		vpX = 0;
		vpY = 0;
		trees = new Tree[numTrees];
		for(int i=0; i<numTrees; i++) {
			trees[i] = new Tree(material);
			trees[i].xpos = Random.Range(-200, 200);
			trees[i].ypos = floor + 5;
			trees[i].zpos = Random.Range(0, 1000);  
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			az = -0.01f;
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			az = 0.01f;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			ax = 0.01f;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			ax = -0.01f;
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			ay = 1.0f;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			az = 0;
		} else if (Input.GetKeyUp(KeyCode.DownArrow)) {
			az = 0;
		} else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			ax = 0;
		} else if (Input.GetKeyUp(KeyCode.RightArrow)) {
			ax = 0;
		} else if (Input.GetKeyUp(KeyCode.Space)) {
			ay = 0;
		}
	}

	void OnPostRender() {
		vx += ax;
		vy += ay;
		vz += az;
		vy += gravity;
		for(int i=0; i<numTrees; i++) {
			Tree tree = trees[i];
			Move(tree);
		}
		vx *= friction;
		vy *= friction;
		vz *= friction;
		SortZ();
	}

	private void Move(Tree tree) {
		tree.xpos += vx;
		tree.ypos += vy;
		tree.zpos += vz;
		if (tree.ypos < floor) {
			tree.ypos = floor;
		}
		if(tree.zpos < -fl) {
			tree.zpos += 100;
		}
		if(tree.zpos > 100 - fl) {
			tree.zpos -= 100;
		}
		float scale = fl / (fl + tree.zpos);
		tree.scaleX = tree.scaleY = scale;
		tree.x = vpX + tree.xpos * scale;
		tree.y = vpY + tree.ypos * scale;
		tree.color = new Color(1, 1, 1, scale);
	}

	private void SortZ() {
		for(int i=0; i<numTrees - 1; i++) {
			for(int j=numTrees-1; j>i; j--) {
				if(trees[j].zpos > trees[j-1].zpos) {
					Tree t = trees[j];
					trees[j] = trees[j-1];
					trees[j-1] = t;
				}
			}
		}
		for(int i=0; i<numTrees; i++) {
			Tree tree = trees[i];
			tree.OnDraw();
		}
	}
}