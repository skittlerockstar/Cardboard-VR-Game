using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	// Use this for initialization
	private float startSpeed =50;
	private Vector3 saveSpeed;
	private Vector3 startpos;
	public bool outOfBounds = false;
	public int winningPlayer;
	public GameObject playfield;
	private Rigidbody rb;
	public GameObject side1;
	public GameObject side2;
	Rigidbody s1;
	Rigidbody s2;
	public AudioClip[] pong;
	void Start () {

		s1 = side1.GetComponent<Rigidbody> ();
		s2 = side1.GetComponent<Rigidbody> ();
		rb = gameObject.GetComponent<Rigidbody>();
		saveSpeed = Vector3.zero;
		startpos = gameObject.transform.position;
	}
	void OnTriggerEnter(Collider other) {
		int sound = Random.Range(0,1);
		if (other.name.Contains ("bat")) {
		
			AudioSource.PlayClipAtPoint(pong[sound],rb.transform.position);
			Rigidbody bat = other.GetComponent<Rigidbody>();
			if(rb.position.z > 0){
				rb.transform.position = new Vector3(rb.transform.position.x,rb.transform.position.y,
				                                    bat.transform.position.z-bat.transform.localScale.z);
			}else{
				rb.transform.position = new Vector3(rb.transform.position.x,rb.transform.position.y,
				                                    bat.transform.position.z+bat.transform.localScale.z);
			}
			float percentage = (other.transform.position.x - gameObject.transform.position.x)*-1;
			rb.velocity =new Vector3( rb.velocity.x,rb.velocity.y,rb.velocity.z*-1.05f);
			rb.AddForce(transform.right*percentage*2,ForceMode.Impulse);
		}
		if (other.name == "side") {
			AudioSource.PlayClipAtPoint(pong[sound],rb.transform.position);
			if(rb.position.x > 0){
				rb.transform.position = new Vector3(s2.transform.position.x-s2.transform.localScale.x-rb.transform.localScale.x
				                                    ,rb.transform.position.y,rb.transform.position.z);
			}else{
				rb.transform.position = new Vector3(s1.transform.position.x+s1.transform.localScale.x+rb.transform.localScale.x
				                                    ,rb.transform.position.y,rb.transform.position.z);
			}
			rb.velocity = new Vector3( rb.velocity.x*-1f,rb.velocity.y,rb.velocity.z);
		}

	}
	void OnTriggerExit(Collider other) {
		if (other.name=="BoundingBox") {
				
			if(rb.position.z > 0){winningPlayer=1;}
			else{winningPlayer=2;}
				
			outOfBounds = true;
			saveSpeed=Vector3.zero;
			rb.transform.position = startpos;
		}
		
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void moveBall(bool t){
	if (t != true) {
			saveSpeed= rb.velocity;
			rb.velocity = new Vector3 (0, 0, 0);

		} else {

			if (rb.velocity.z == 0 ) {
				if(saveSpeed == Vector3.zero){
				rb.AddForce(new Vector3(0,0,50f),ForceMode.Impulse);
				}else{
					rb.velocity = saveSpeed;
				}
			}
			gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x,-150,150),
			                                            gameObject.transform.position.y,
			                                            gameObject.transform.position.z);
			if(gameObject.transform.position.x >= 140 || gameObject.transform.position.x <= -140){
				rb.velocity =
					new Vector3(
						rb.velocity.x*-1,
						rb.velocity.y,
						rb.velocity.z);
			}
		}

	}
}
