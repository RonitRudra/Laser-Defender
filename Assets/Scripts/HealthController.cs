using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public RawImage healthBar;
	public GameObject restartDialog;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		changeHealth();
		showRestartDialog(false);
	}
	
	public void changeHealth(){
		float health = GameObject.Find("Player").GetComponent<PlayerController>().health;
		healthBar.rectTransform.sizeDelta = new Vector2(health,healthBar.rectTransform.sizeDelta.y);
	}
	
	public void showRestartDialog(bool dead){
		if(dead){
			Time.timeScale = 0.0f;
		}
		else if(!dead){
			Time.timeScale = 1.0f;
		}
		restartDialog.SetActive(dead);
	}
}
