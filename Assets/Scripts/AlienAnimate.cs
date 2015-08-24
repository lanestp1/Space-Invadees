using UnityEngine;
using System.Collections.Generic;

public class AlienAnimate : MonoBehaviour {
	public List<MeshFilter> meshes;
	public bool shouldAnimate = true;
	
	private Mesh mesh;
	private float animationMin = .2f;
	private float animationCur;
	private int animationFrame;
	private bool isForward;
	
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		animationFrame = Random.Range(0, meshes.Count);
		animationCur = animationMin;//Random.Range(animationMin,animationMax);
		mesh = meshes[animationFrame].sharedMesh;
		if (animationFrame == 0) {
			isForward = true;
		}
		else if (animationFrame == meshes.Count-1) {
			isForward = false;
		}
		else {
			isForward = (Random.Range(0,1) < .5);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!shouldAnimate) {
			return;
		}
		animationCur-=Time.deltaTime;
		if (animationCur <= 0) {
			animationCur = animationMin;//Random.Range(animationMin,animationMax);
			animationFrame += (isForward) ? 1 : -1;
			GetComponent<MeshFilter>().mesh = meshes[animationFrame].sharedMesh;
			if (animationFrame == 0) {
				isForward = true;
			}
			else if (animationFrame == meshes.Count-1) {
				isForward = false;
			}
		}
	}
}
