using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	public GameObject EnemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	private float xmax;
	private float xmin;
	private bool movingRight = true;
	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		Spawner();
		}
	
	public void OnDrawGizmos(){
	Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += new Vector3(speed*Time.deltaTime,0);
		}
		else{
			transform.position += new Vector3(-speed*Time.deltaTime,0);
		}
		
		float rightEdgeOfFormation = transform.position.x+0.5f*width;
		float leftEdgeOfFormation = transform.position.x-0.5f*width;
		if(leftEdgeOfFormation <= xmin){
			movingRight = true;
		}
		else if(rightEdgeOfFormation >= xmax){
			movingRight = false;
		}
		
		if(AllMembersDead()){
			Debug.Log("All Members Dead!");
			SpawnUntilFull();
		}
	
	}
	
	bool AllMembersDead(){
	
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
	
	void Spawner(){
		foreach( Transform child in transform){
			GameObject enemy = Instantiate(EnemyPrefab, child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	Transform nextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	
	}
	
	void SpawnUntilFull(){
		Transform freePosition = nextFreePosition();
		if(freePosition){
		GameObject enemy = Instantiate(EnemyPrefab, freePosition.position,Quaternion.identity) as GameObject;
		enemy.transform.parent = freePosition;
		}
		if(nextFreePosition()){
			Invoke ("SpawnUntilFull",spawnDelay);
			}
	}
}
