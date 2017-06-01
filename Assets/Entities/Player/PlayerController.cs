using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	#region Global Variables
	public float speed = 10f;
	public float padding = 0.5f;
	public GameObject Laser;
	public float ProjectileSpeed = 0.5f;
	public float FireRate = 0.2f;
	public AudioClip fireSound;
	float xmin;
	float xmax;
	float ymin;
	float ymax;
	public float health = 300f;
	#endregion
	// Use this for initialization
	void Start () {
		float zDistance = transform.position.z-Camera.main.transform.position.z;
		Vector3 LeftMost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,zDistance));
		Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f,0f,zDistance));
		//Vector3 TopMost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,zDistance));
		//Vector3 BottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0.25f,zDistance));
		
		xmin = LeftMost.x + padding;
		xmax = RightMost.x - padding;
		//ymin = BottomMost.y;
		//ymax = TopMost.y;
		
	}
	
	// Update is called once per frame
	void Update () {
		KeyControl();
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("LaserFire",0.000001f,FireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("LaserFire");
		}
	
	}
	
	void KeyControl(){
		// Since object is kinematic, transform position directly
		// Movement Speed independent of framerate
		// Could also use Vector3.left etc which is a unit vector
		if(Input.GetKey(KeyCode.A)){
			gameObject.transform.position += new Vector3(-speed*Time.deltaTime,0,0);
		}
		else if(Input.GetKey(KeyCode.D)){
			gameObject.transform.position += new Vector3(speed*Time.deltaTime,0,0);
			
		}
		else if(Input.GetKey(KeyCode.W)){
			gameObject.transform.position += new Vector3(0,speed*Time.deltaTime,0);
			
		}
		else if(Input.GetKey(KeyCode.S)){
			gameObject.transform.position += new Vector3(0,-speed*Time.deltaTime,0);
			
		}
		
		// restrict player to gamespace
		float newx = Mathf.Clamp(transform.position.x,xmin,xmax);
		//float newy = Mathf.Clamp(transform.position.y,ymin,ymax);
		
		transform.position = new Vector3(newx,transform.position.y,transform.position.z);

	
		}
	
	void LaserFire(){
		GameObject beam;
		beam = Instantiate(Laser,transform.position,Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,ProjectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="Laser_Enemy"){
			Projectile missile = col.gameObject.GetComponent<Projectile>();
			health -=missile.GetDamage();
			missile.Hit();
			if (health <=0){
				GetComponent<HealthController>().changeHealth();
				Destroy (gameObject);
				GetComponent<HealthController>().showRestartDialog(true);
			}
					
		}
		
	}
	
}
	
	 
