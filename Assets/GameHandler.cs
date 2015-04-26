using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHandler : MonoBehaviour {

	private enum Gamestate{Main,Pause,Play,Ready,GameOver} ;
	Gamestate myGamestate;
	bool isPlaying;
	public Canvas canvas;
	public GameObject test;
	public GameObject ball;
	public GameObject bat1;
	public GameObject bat2;
	public GameObject playfield;
	public GameObject mainMenu;
	public GameObject readyMenu;
	public GameObject scoreMenu;
	public GameObject pausedMenu;
	public GameObject gameOverMenu;
	private string canvasPosition;
	public AudioClip[] audClips;
	Component cc;
	GameObject c;
	Vector2 Score = new Vector2(0,0);
	Vector3 startPos1;
	Vector3 startPos2;
	public AudioSource auS;
	// Use this for initialization
	void OnEnable(){
		MagnetSensor.OnCardboardTrigger += handleClick;
	}
	void OnDisable(){
	//	MagnetSensor.OnCardboardTrigger -= handleClick;
	}	
	void Start () {
		isPlaying = false;
		//handleMain ();
		myGamestate = Gamestate.Main;
		canvasPosition = "center";
		 cc = canvas.GetComponent<RectTransform> ();
		 c = GameObject.Find ("ARCamera2");
		startPos1 = bat1.transform.position;
		startPos2 = bat2.transform.position;
		auS.clip =audClips [Random.Range (0,audClips.Length-1)];
		auS.Play ();
	}
	
	// Update is called once per frame
	void FixedUpdate(){
	
		if (c.GetComponent<TrackableList>().isTracking != false) {

			//move ball
			ball.GetComponent<collision> ().moveBall (isPlaying);
			//player movement
			if(bat1 !=null){
			bat1.GetComponent<playerMover> ().moveBat (isPlaying);
			}
			moveCanvas(canvasPosition);
		}else{
			if(myGamestate == Gamestate.Play){
				handlePlay();
			}
		}
		//canvas always facing camera
		cc.transform.localRotation = c.transform.rotation;

	}
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			handleClick ();
			Debug.Log ("click");
		}
		gameStateEvents ();
		if (auS.isPlaying) {
			Debug.Log(auS.time);
			if(auS.time > auS.clip.length-2){
				auS.clip =audClips [Random.Range (0,audClips.Length-1)];
				auS.Play ();
			}
		}
	}
	void handleClick(){
		Debug.Log ("in handleclick");
		if (myGamestate == Gamestate.Main) {
			handleMain();
		}
		else if (myGamestate == Gamestate.Pause) {
			handlePause();
		}
		else if (myGamestate == Gamestate.Play) {
			handlePlay();
		}
		else if (myGamestate == Gamestate.Ready) {
			handleReady();
		}
		else if (myGamestate == Gamestate.GameOver) {
			handleGameOver();
		}
	
	}
	void gameStateEvents(){
	switch (myGamestate) {
		case Gamestate.Main:
			break;
		case Gamestate.Ready:
			break;
		case Gamestate.Pause:
			break;
		case Gamestate.Play:
			if(ball.GetComponent<collision> ().outOfBounds){

				if(ball.GetComponent<collision> ().winningPlayer == 1){
					Score = new Vector2( Score.x+1,Score.y);
				}else{
					Score = new Vector2( Score.x,Score.y+1);
				}if(Score.x > 4 || Score.y >4 ){
					handleWin();
				}else{
				GameObject.Find("playerScore").GetComponent<Text>().text = Score.x.ToString();
				GameObject.Find("cpuScore").GetComponent<Text>().text = Score.y.ToString();
				bat1.transform.position = startPos1;
				bat2.transform.position = startPos2;
				ball.GetComponent<collision> ().outOfBounds = false;
				handleMain();
				}
			}
			break;
		case Gamestate.GameOver:
			break;
		}
	}
	void handleMain(){
		if (!isPlaying) {
			scaleCanvas ();
			mainMenu.SetActive (false);
			readyMenu.SetActive (true);
		} else {
			isPlaying = false;
		}
		myGamestate = Gamestate.Ready;
		canvasPosition = "right";
	}
	void handlePause(){
		isPlaying = true;
		myGamestate = Gamestate.Play;
		scaleCanvas ();
		scoreMenu.SetActive (true);
		pausedMenu.SetActive (false);
		canvasPosition = "right";
	}
	void handleReady(){
		myGamestate = Gamestate.Play;
		scaleCanvas ();
		readyMenu.SetActive (false); 
		scoreMenu.SetActive (true); 
		canvasPosition = "right";
		isPlaying = true;
	}
	void handlePlay(){
		myGamestate = Gamestate.Pause;
		scaleCanvas ();
		scoreMenu.SetActive (false); 
		pausedMenu.SetActive (true); 
		canvasPosition = "center";
		isPlaying = false;
	}
	void handleGameOver(){
		myGamestate = Gamestate.Main;
		scaleCanvas ();
		mainMenu.SetActive (true); 
		gameOverMenu.SetActive (false); 
	}
	void moveCanvas(string pos){
		float step = 100 * Time.deltaTime;
		Component cc = canvas.GetComponent<RectTransform> ();
		if (pos == "center") {
			cc.transform.position = Vector3.MoveTowards (cc.transform.position, 
		                                            new Vector3 (0,
		            cc.transform.position.y,
		            cc.transform.position.z),
		                                            step);

		}
		 else if (pos == "right") {
			cc.transform.position = Vector3.MoveTowards (cc.transform.position, 
			                                             new Vector3 (-150,
			             cc.transform.position.y,
			             cc.transform.position.z),
			                                             step);
			
		}
	}
	void scaleCanvas(){
		RectTransform can = GameObject.Find ("Canvas").GetComponent<RectTransform>();
		if (myGamestate == Gamestate.Play) {
			can.sizeDelta= new Vector2( 200,60);
		} else {
			can.sizeDelta= new Vector2( 500,200);
		}
	}

	void handleWin(){
		isPlaying = false;
		myGamestate = Gamestate.GameOver;
		scoreMenu.SetActive(false);
		gameOverMenu.SetActive(true);
		if(Score.x > Score.y){
	
			GameObject.Find("GameOver").GetComponent<Text>().text =  "You Win !";
		}else{
			GameObject.Find("GameOver").GetComponent<Text>().text =  "You Lose !";
		}
		canvasPosition= "center";
		scaleCanvas ();
		bat1.transform.position = startPos1;
		bat2.transform.position = startPos2;
		Score = new Vector2 (0, 0);
		scoreMenu.SetActive(false);
		gameOverMenu.SetActive(true);
	}

}

