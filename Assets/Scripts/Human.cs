using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Human : MonoBehaviour {
	public GameObject model;
	public List<BulletScript> Bullets;
	public List<ParticleSystem> Booms;
	public bool isDead = false;
	public enum HumanEnum {
		PeaceShip
	}
	public HumanEnum HumanType = HumanEnum.PeaceShip;
	/*public void shoot() {
		if (isDead)
			return;
		foreach (var item in Bullets)
		{
			if (item.fire()) {
				return;
			}
		}
	}*/
	public void Start(){
		switch (HumanType){
			case HumanEnum.PeaceShip :
				model.transform.DORotate(new Vector3(0,360,0),10.0f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
				break;
		}
	}
	public void explode() {
		Camera.main.GetComponent<MainScript>().playBoom();
		isDead = true;
		model.gameObject.SetActive(false);
		foreach (var item in Booms)
		{
			item.Play();
		}
		foreach (var item in Bullets)
		{
			item.disable();
		}
	}
	
	public void OnTriggerEnterParent(Collider other) {
		Debug.Log(other.tag);
		if (!isDead && other.tag == "alien") {
			Debug.Log ("boom");
			explode();
		}
	}

}
