using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public float speed = 0.5f;
	private float acceleration = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Handle input
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			speed += (acceleration * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			speed -= (acceleration * Time.deltaTime);
		}

		//Scroll the road and its children
		Vector2 offset = new Vector2 (0, Time.time * speed);

		gameObject.GetComponent<Renderer> ().material.mainTextureOffset = offset;
		Renderer[] children = gameObject.GetComponentsInChildren<Renderer> ();

		for (int i = 0; i < children.Length; i++) {
			children[i].material.mainTextureOffset = offset;
		}
	}
}
