using UnityEngine;
using System.Collections;

public class cpuMover : MonoBehaviour {

	public GameObject ball;
	// Use this for initialization
	void Start () {
	
	}
	void FixedUpdate(){
		float r = Random.Range (50, 100);
		float step = r * Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards (
			gameObject.transform.position, new Vector3 (
			Mathf.Clamp (ball.transform.position.x, -150, 150),
			gameObject.transform.position.y,
			gameObject.transform.position.z
		), step);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
