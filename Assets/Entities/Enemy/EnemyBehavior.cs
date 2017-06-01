using UnityEngine;
using System.Collections;


public class EnemyBehavior : MonoBehaviour {

	private float health = 150f;
	public GameObject Laser;
	private float Damage = 50f;
	
	public float ProjectileSpeed = 5f;
	public float ShotsPerSec = 0.25f;
	
	public int scoreValue = 150;
	
	private ScoreKeeper scoreKeeper;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="Laser_Player"){
			Projectile missile = col.gameObject.GetComponent<Projectile>();
			health -=missile.GetDamage();
			missile.Hit();
			if (health <=0){
				Destroy (gameObject);
				AudioSource.PlayClipAtPoint(deathSound,transform.position);
				scoreKeeper.Score(scoreValue);
			}
			//Debug.Log("Laser Hit!! " + missile.Damage);
		}
	
	}
	
	void Update(){
	float prob = ShotsPerSec * Time.deltaTime;
		if(Random.value < prob){
			LaserFire();
		}
	
	}
	
	void LaserFire(){
		GameObject beam = Instantiate(Laser,transform.position,Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,-ProjectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
		
	}
	
	float GetDamage(){
		return Damage;
	}
}
