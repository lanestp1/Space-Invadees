using UnityEngine;
using System.Collections;

public class HumanHitTrigger : MonoBehaviour {
	public Human alien;

	public void OnTriggerEnter(Collider other) {
		alien.OnTriggerEnterParent(other);
	}
	public void OnTriggerStay(Collider other) {
		alien.OnTriggerEnterParent(other);
	}
	public void OnTriggerExit(Collider other) {
		alien.OnTriggerEnterParent(other);
	}

}
