using UnityEngine;
using System.Collections;
public class StereoHandler : MonoBehaviour {

	GameObject leftPlane;
	Camera leftCam;
	Camera rightCam;
	Camera arCam;
	private Vector3 camPos;
	// Use this for initialization
	void Start () {
		 leftPlane = GameObject.Find ("Left");
		leftCam = GameObject.Find ("CamLeft").GetComponent<Camera>(); 
		rightCam = GameObject.Find ("CamRight").GetComponent<Camera>();
		arCam = GameObject.Find ("arCam").GetComponent<Camera>();
		arCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
			setPlane ();
	}
	private void setPlane(){
		leftPlane.transform.localPosition = new Vector3 (1250, 0, leftPlane.transform.localPosition.z);
	}

}
