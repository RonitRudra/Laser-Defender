using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public int score;
	private Text myText;
	
	public void Score(int points){
		score += points;
		myText.text = score.ToString();
	}
	
	public void Reset(){
		score = 0;
		myText.text = score.ToString();
	}
	
	void Start(){
		myText = GetComponent<Text>();
		Reset ();
		
	}
}
