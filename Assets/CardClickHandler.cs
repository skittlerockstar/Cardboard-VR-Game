using UnityEngine;
using System.Collections;

public class CardClickHandler : MonoBehaviour {

	void OnEnable(){
		MagnetSensor.OnCardboardTrigger += test;
	}
	void OnDisable(){
		MagnetSensor.OnCardboardTrigger -= test;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void test(){
	
	}
}
