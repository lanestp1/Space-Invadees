using UnityEngine;
using System.Collections;

public class AlienHitTrigger : MonoBehaviour {
	public Alien alien;
	public bool isInstantDeath = false;
	public void OnTriggerEnter(Collider other) {
		alien.OnTriggerEnterParent(other,isInstantDeath);
	}
	public void OnTriggerStay(Collider other) {
		alien.OnTriggerEnterParent(other,isInstantDeath);
	}
	public void OnTriggerExit(Collider other) {
		alien.OnTriggerEnterParent(other,isInstantDeath);
	}
}
