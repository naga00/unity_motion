using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light {
	public float x, y, z;
	public float brightness;

	public Light(float x, float y, float z, float brightness) {
		this.x = x;
		this.y = y;
		this.z = z;
		this.brightness = brightness;  
	}

	public void setBrightness(float b) {
		brightness = Mathf.Max(b, 0);
		brightness = Mathf.Min(brightness, 1);
	}
}