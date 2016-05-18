using UnityEngine;
using System.Collections;

public class RoadLine : MonoBehaviour {

	private Transform myTransform;
	private float speed = 1.0f;
	private float maxDistance = 5.11f;
	private float dist = 0.0f;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Handle input
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			myTransform.localPosition += Vector3.left * (speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			myTransform.localPosition += Vector3.right * (speed * Time.deltaTime);
		}

		//Clamp distance of line movement
		if (myTransform.position.x > maxDistance) {
			myTransform.position = new Vector3(
				maxDistance, myTransform.position.y, myTransform.position.z);
		} else if (myTransform.position.x < -maxDistance) {
			myTransform.position = new Vector3(
				-maxDistance, myTransform.position.y, myTransform.position.z);
		}
	}

	public float getNormalisedDistance(){
		//Distance between line and CarFuzz
		dist = GameObject.Find ("CarFUZZ").GetComponent<Transform> ().position.x - 
			myTransform.position.x;
		float v = (dist - (-maxDistance*2)) / 
			(maxDistance*2 - (-maxDistance*2));
		return 2 * (v - 0.5f);
	}
}
