using UnityEngine;
using System.Collections;

public class antiglitch : MonoBehaviour {
	
	// Use this for initialization
	public GUISkin gskin;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		GUI.skin = gskin;
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		GUI.Box(new Rect(0,0,Screen.width,Screen.height/5),"");
		//GUI.Box(new Rect(0,0,Screen.width,300),"");
		GUI.Box(new Rect(0,Screen.height-Screen.height/4,Screen.width,Screen.height/4),"");
		GUI.EndGroup ();
	}
}
