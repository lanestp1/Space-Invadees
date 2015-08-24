using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

	public enum GameStateEnum {
		Starting,
		Running,
		Pausing,
		Paused,
		Unpausing,
		Won,
		WonDone,
		Lost,
		LostDone
	}
	public enum Dir {
		left,
		right
	}

public class MainScript : MonoBehaviour {
	public AudioClip pew;
	public AudioClip boom;
	private AudioSource audio1;
	public GameStateEnum GameState = GameStateEnum.Running;
	public List<Alien> Aliens;
	public List<Human> Humans;
	
	public Dir goalDirection;
	public Dir movementDirection = Dir.left;
	
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject highlight;
	public GameObject pauseCanvas;
	
	public GameObject Victory;
	public GameObject Defeat;
	public int levelNum;
	private bool hasHit = false;
	public void playBoom(){
		audio1.PlayOneShot(boom,0.5f);
	}
	public void playPew(){
		audio1.PlayOneShot(pew,0.5f);
	}
	
	public void RestartButton() {
		if (Debug.isDebugBuild){
			GameState = GameStateEnum.Won;
		}
		else {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	public void PauseButton(){
		if (GameState == GameStateEnum.Running)
			GameState = GameStateEnum.Pausing;
	}	
	public void ResumeButton(){
		if (GameState == GameStateEnum.Paused)
			GameState = GameStateEnum.Unpausing;
	}	
	public void checkLoss() {
			foreach (var item in Aliens) {
				if (item.AlienState == AlienStateEnum.Alive) {
					return;
				}
			}
			Debug.Log("Lost");
			GameState = GameStateEnum.Lost;
	}
	public void hitSide (string tag) {
		bool shouldMove = false;
		if (tag == "human") {
			foreach (var item in Humans) {
				if (!item.isDead)
					return;
			}			
			GameState = GameStateEnum.Won;
		}
		else if (tag == "bullet") {
			foreach (var item in Aliens) {
				if (item.AlienState == AlienStateEnum.Alive) {
					return;
				}
			}
			GameState = GameStateEnum.Lost;
		}
		else if (tag == "left") {
			movementDirection = Dir.right;
			hasHit = true;
			if (goalDirection == Dir.left) {
				goalDirection = Dir.right;
				shouldMove = true;
				setGoal();
			}
		}
		else if (tag == "right") {
			movementDirection = Dir.left;
			hasHit = true;
			if (goalDirection == Dir.right) {
				goalDirection = Dir.left;
				shouldMove = true;
				setGoal();
			}
		}
		if (shouldMove) {
			foreach (var item in Aliens){
				var p = item.gameObject.transform.position;
				item.gameObject.transform.DOMoveY(p.y-1,0.5f);
			}
		}
	}
	void setGoal() {
		if (goalDirection == Dir.right) {
			highlight.transform.position = new Vector3(rightWall.transform.position.x,highlight.transform.position.y,highlight.transform.position.z);
		}
		else {
			highlight.transform.position = new Vector3(leftWall.transform.position.x,highlight.transform.position.y,highlight.transform.position.z);
		}
	}
	// Use this for initialization
	void Start () {
		audio1 = Camera.main.GetComponent<AudioSource>();
		setGoal();
		leftWall.transform.DOMoveX(-20,0.75f).From();
		rightWall.transform.DOMoveX(20,0.75f).From();
		foreach (var item in Aliens)
		{
			item.transform.DOMoveY(20,1.0f).From();
		}
		foreach (var item in Humans)
		{
			item.transform.DOMoveY(-20,1.0f).From();
		}
		
		PlayerPrefs.SetInt("level",levelNum);
		PlayerPrefs.SetInt("maxLevel",levelNum-1);
		if (levelNum == 1) {
			GameState = GameStateEnum.Paused;
			pauseCanvas.SetActive(true);
			Time.timeScale = 0;
		}
		else {
		GameState = GameStateEnum.Running;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (GameState) {
			case GameStateEnum.Lost :
				Lost();
				break;
			case GameStateEnum.Paused :
				Paused();
				break;
			case GameStateEnum.Pausing :
				Pausing();
				break;
			case GameStateEnum.Running :
				Running();
				break;
			case GameStateEnum.Starting :
				Starting();
				break;
			case GameStateEnum.Unpausing :
				Unpausing();
				break;
			case GameStateEnum.Won :
				Won();
				break;
			case GameStateEnum.WonDone :
				WonDone();
				break;
			case GameStateEnum.LostDone :
				LostDone();
				break;
		}

	}
	void Starting() {
			
	}
	void Running() {
		if (hasHit) {
			hasHit = false;
			return;
		}
		if (Input.GetAxis("Horizontal") < -.1f) {
			movementDirection = Dir.left;
		}
		else if (Input.GetAxis("Horizontal") > .1f) {
			movementDirection = Dir.right;
		}
	}
	void Pausing() {
		GameState = GameStateEnum.Paused;
		DOTween.To(()=> Time.timeScale,x=>Time.timeScale = x,0.0f,0.3f).OnComplete(()=> {Debug.Log("Paused"); });
		StartCoroutine(PausingIEnum());
	}
	IEnumerator PausingIEnum() {
		float pauseEndTime = Time.realtimeSinceStartup + 0.5f;
    	while (Time.realtimeSinceStartup < pauseEndTime)
    	{
        	yield return 0;
    	}
		pauseCanvas.SetActive(true);
	}
	void Paused() {
			
	}
	void Unpausing() {
		GameState = GameStateEnum.Running;
		pauseCanvas.SetActive(false);
		Time.timeScale = .01f;
		DOTween.To(()=> Time.timeScale,x=>Time.timeScale = x,1.0f,0.3f).OnComplete(()=> {});
			
	}
	void Won() {
		Victory.gameObject.SetActive(true);
		Victory.transform.DOScale(new Vector3(0,0,0),1.5f).From();
		GameState = GameStateEnum.WonDone;
	}
	void Lost() {
		Defeat.gameObject.SetActive(true);
		Defeat.transform.DOScale(new Vector3(0,0,0),1.5f).From();
		GameState = GameStateEnum.LostDone;			
	}
	void LostDone() {
		if (Input.GetMouseButtonDown(0))
			RestartButton();
	}
	void WonDone() {
		PlayerPrefs.SetInt("maxLevel",levelNum);
		PlayerPrefs.SetInt("level",levelNum+1);


		if (Input.GetMouseButtonDown(0)){
			if (levelNum == 9)
				Application.LoadLevel(0);	
			else
				Application.LoadLevel(levelNum+1);	
		}
	}

}
