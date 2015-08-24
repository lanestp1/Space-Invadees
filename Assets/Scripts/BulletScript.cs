using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Transform Barrier;
	public Transform Human;
	public ParticleSystem trail;
	public float initialDelay = 2.0f;
	private float currWaitTime;
	private enum BulletStateEnum {
		Available,
		ShouldShoot,
		Shooting,
		Hit,
		Waiting
	}
	BulletStateEnum BulletState = BulletStateEnum.Available;
	public void disable() {
		BulletState = BulletStateEnum.Available;
	}
	
	public bool fire(){
		if (BulletState == BulletStateEnum.Available) {
			BulletState = BulletStateEnum.ShouldShoot;
			return true;
		}
		return false;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "alien") {
			BulletState = BulletStateEnum.Hit;
		}
	}

	// Use this for initialization
	void Start () {
		//fire();
		StartCoroutine(initialFire());
	}
	
	IEnumerator initialFire(){
		yield return new WaitForSeconds(initialDelay);
		BulletState = BulletStateEnum.ShouldShoot;
		yield break;
	}
	// Update is called once per frame
	void Update () {
		switch 	(BulletState) {
		case BulletStateEnum.Available:
			Available();
			break;
		case BulletStateEnum.ShouldShoot:
			ShouldShoot();
			break;
		case BulletStateEnum.Shooting:
			Shooting();
			break;
		case BulletStateEnum.Hit:
			Hit();
			break;
		case BulletStateEnum.Waiting:
			Waiting();
			break;
		}	
	}

	void Available() {
		transform.position=new Vector3(1000,1000,1000);
		//gameObject.SetActive(false);
	}
	
	void ShouldShoot() {
		Debug.Log("Shoot");
		transform.position = Human.position;
		//gameObject.SetActive(true);
		trail.Play();
		Camera.main.GetComponent<MainScript>().playPew();
		BulletState = BulletStateEnum.Shooting;
	}
	
	public float bulletSpeed = 3.5f;
	public float waitingTime = 0.5f;
	
	void Shooting () {
		transform.position = new Vector3(transform.position.x,transform.position.y+Time.deltaTime*bulletSpeed,transform.position.z);
		if (transform.position.y >= Barrier.position.y) {
			BulletState = BulletStateEnum.Hit;
		}
	}
	void Hit () {
		trail.Stop();
		transform.position = new Vector3(-1000,-1000,-1000);
		BulletState = BulletStateEnum.Waiting;
		currWaitTime = waitingTime;
	}
	void Waiting() {
		currWaitTime-=Time.deltaTime;
		if (currWaitTime <= 0) {
			BulletState = BulletStateEnum.ShouldShoot;
		}
	}
}
