using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		Rigidbody r= gameObject.GetComponent<Rigidbody> ();
		r.AddForce (new Vector3 (1,0,0));
	}
	void Update () {
	
	}
}
