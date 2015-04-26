using UnityEngine;
using System.Collections;

public class playerMover : MonoBehaviour {

	public GameObject bounding;
	public float speed;
	public bool isAtCenter;
	public GameObject playfield;
	public GameObject playerBat;
	public Camera someCam;
	// Use this for initialization
	void Start () {
		isAtCenter = false;
		bounding = GameObject.Find ("viewmouse");
		someCam = GameObject.Find ("CamLeft").GetComponent<Camera>(); 
	}
	
	public void moveBat(bool playing){
	
		if (playing == true) {
			if(!isAtCenter){
				
				Ray r =  someCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
				RaycastHit rch;
				if (Physics.Raycast(r, out rch)){
					float posx = Mathf.Clamp(rch.point.x
					                         ,-150,150);
					playerBat.transform.position = new Vector3(
						posx,playerBat.transform.position.y,playerBat.transform.position.z);
				}

			}
		}
		//gameObject.GetComponent<Rigidbody>().velocity = new Vector3(speed,0,0);
	}
	void OnTriggerEnter(Collider other) {
		if (other.name == "viewmouse") {
			isAtCenter=true;
		}
	}
	void onTriggerLeave(Collider other) {
		if (other.name == "viewmouse") {
			isAtCenter=false;
		}
		if (other.name == "BoundingBox") {
		
		}
	}
}
