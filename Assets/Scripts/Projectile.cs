using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private float Damage = 50f;
	
	public float GetDamage(){
		return Damage;
	}
	
	public void Hit(){
		Destroy (gameObject);
	}
}
