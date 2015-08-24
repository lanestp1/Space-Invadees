using UnityEngine;
using System.Collections;

public class InstantDeath : MonoBehaviour {
	private MainScript mainScript;
	// Use this for initialization
	void Start () {
		mainScript = Camera.main.GetComponent<MainScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
