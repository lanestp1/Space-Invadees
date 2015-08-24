using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

	public enum AlienStateEnum {
		Alive,
		Dying,
		Dead,
		Won
	}

public class Alien : MonoBehaviour {
	public List<ParticleSystem> deathParticles;
	public GameObject model;
	private MainScript mainScript;
	public AlienStateEnum AlienState = AlienStateEnum.Alive;
	
	// Use this for initialization
	void Start () {
		mainScript = Camera.main.GetComponent<MainScript>();
		
	}
	
	public void OnTriggerEnterParent(Collider other, bool isInstantDeath = false) {
		if (AlienState == AlienStateEnum.Alive && other.tag == "bullet") {
			AlienState = AlienStateEnum.Dying;
			if (isInstantDeath) {
				foreach (var item in mainScript.Aliens)
				{
					item.AlienState = AlienStateEnum.Dying;
				}
			}
			mainScript.checkLoss();
		}
		else if (AlienState == AlienStateEnum.Alive && other.tag == "death") {
			AlienState = AlienStateEnum.Dying;
			mainScript.checkLoss();
		}
		else {
			mainScript.hitSide(other.tag);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		switch (AlienState) {
			case AlienStateEnum.Alive :
				Alive();
				break;
			case AlienStateEnum.Dying :
				Dying();
				break;
			case AlienStateEnum.Dead :
				Dead();
				break;
			case AlienStateEnum.Won :
				break;
		}
	}
	
	public float movmentMultiplier = 2.5f;
	void Alive() {
		//Do animations
		switch (mainScript.GameState) {
			case GameStateEnum.Lost :
				AlienState = AlienStateEnum.Dying;
				break;
			case GameStateEnum.Paused :
				break;
			case GameStateEnum.Pausing :
				break;
			case GameStateEnum.Running :
				transform.position=new Vector3(((mainScript.movementDirection == Dir.left) ? -1 : 1) * movmentMultiplier*Time.deltaTime +transform.position.x,transform.position.y,transform.position.z);
				break;
			case GameStateEnum.Starting :
				break;
			case GameStateEnum.Unpausing :
				break;
			case GameStateEnum.Won :
				transform.DOMove(new Vector3(0,-0,-100),30.0f);
				AlienState = AlienStateEnum.Won;
				break;
		}
	}
	
	void Dying () {
		mainScript.playBoom();
		model.SetActive(false);
		foreach (var item in deathParticles)
		{
			item.Play();
		}
		AlienState = AlienStateEnum.Dead;
	}
	
	void Dead () {
		//Nothing to see here
	}
}
