using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MainMenu : MonoBehaviour {
	public List<GameObject> topItems;
	public List<GameObject> bottomItems;
	public List<GameObject> aliens;
	public List<GameObject> settings;
	public List<GameObject> game;
	public GameObject ship;
	public GameObject soundOn;
	public GameObject soundOff;
	public GameObject planet;
	public List<GameObject> meshes;
	private Mesh mesh;

	void Awake() {
		Vector3 pos = planet.transform.position;
		Quaternion rot = planet.transform.rotation;
		Vector3 sca = planet.transform.localScale;
		DestroyImmediate(planet);
		//PlayerPrefs.SetInt("maxLevel",0);
		//PlayerPrefs.SetInt("level",1);
		int maxLevel = PlayerPrefs.GetInt("maxLevel",0);
		maxLevel = (maxLevel > 9) ? 9 : maxLevel;
		PlayerPrefs.SetInt("maxLevel",maxLevel);
		planet = Instantiate(meshes[PlayerPrefs.GetInt("maxLevel",0)]);
		planet.transform.position = pos;
		planet.transform.rotation = rot;
		planet.transform.localScale = sca;
		planet.tag = "playgame";
	}
	// Use this for initialization
	void Start () {
		float v = PlayerPrefs.GetFloat("volume",0.5f);
		AudioListener.volume = v;
		soundOff.SetActive((v < .25f));
		soundOn.SetActive((v > .25f));
		StartCoroutine (StartIn());
	}
	
	IEnumerator StartIn() {
		ship.transform.DOLocalRotate(new Vector3(0,360,0),6.0f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
		//ship.transform.DOShakeRotation(4.0f,new Vector3(20,0,20),0,90).SetEase(Ease.InOutSine).SetLoops(-1);
		foreach (var item in topItems)
		{
			item.transform.DOMoveX(item.transform.position.x-20,4.0f);
		}
		yield return new WaitForSeconds(.4f);
		foreach (var item in bottomItems)
		{
			yield return new WaitForSeconds(.1f);
			item.transform.DOMoveX(item.transform.position.x-20,4.0f);
		}
		yield return new WaitForSeconds(.4f);
		foreach (var item in aliens)
		{
			yield return new WaitForSeconds(.1f);
			item.transform.DOMoveX(item.transform.position.x-20,4.0f);
		}
				
	}
	
	void SettingsIn(){
		DOTween.CompleteAll();
		foreach (var item in topItems)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		foreach (var item in bottomItems)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		foreach (var item in aliens)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		foreach (var item in settings)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
	}
	
	void SettingsOut(){
		DOTween.CompleteAll();
		foreach (var item in topItems)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		foreach (var item in bottomItems)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		foreach (var item in aliens)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		foreach (var item in settings)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
	}

	void GameIn(){
		DOTween.CompleteAll();
		foreach (var item in topItems)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		foreach (var item in bottomItems)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		foreach (var item in aliens)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		foreach (var item in game)
		{
			item.transform.DOMoveX(item.transform.position.x+30,4.0f);
		}
		planet.transform.DOMoveX(planet.transform.position.x+30,4.0f);
		StartCoroutine(GameInCoroutine());
	}
	IEnumerator GameInCoroutine() {
		yield return new WaitForSeconds(3.0f); 
		planet.transform.DORotate(new Vector3(0,180,0),2.0f,RotateMode.LocalAxisAdd);
	}
	
	void GameOut(){
		DOTween.CompleteAll();
		foreach (var item in topItems)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		foreach (var item in bottomItems)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		foreach (var item in aliens)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		foreach (var item in game)
		{
			item.transform.DOMoveX(item.transform.position.x-30,4.0f);
		}
		planet.transform.DOMoveX(planet.transform.position.x-30,4.0f);
		planet.transform.DORotate(new Vector3(0,-180,0),2.0f,RotateMode.LocalAxisAdd);
	}
	
	IEnumerator PlayGame() {
		int level = PlayerPrefs.GetInt("level",1);
		if (level < 10){
			planet.transform.DORotate(new Vector3(0,-180,0),2.0f,RotateMode.LocalAxisAdd);
			yield return new WaitForSeconds(2.0f);
			Application.LoadLevel(level);	
		}		
	}
	
	private string lastTag="";
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				Debug.Log(hit.collider.gameObject.tag);
				if (hit.collider.gameObject.tag == lastTag){
					return;
				}
				
				if (hit.collider.gameObject.tag == "settings") {
					SettingsIn();
				}
				else if (hit.collider.gameObject.tag == "newgame") {
					GameIn();
				}
				else if (hit.collider.gameObject.tag == "sound") {
					float v = PlayerPrefs.GetFloat("volume",0.5f);
					v = (v > .25f) ? 0 : 0.5f;
					soundOff.SetActive((v < .25f));
					soundOn.SetActive((v > .25f));
					PlayerPrefs.SetFloat("volume",v);
					AudioListener.volume = v;
				}
				else if (hit.collider.gameObject.tag == "settingsback") {
					SettingsOut();
				}
				else if (hit.collider.gameObject.tag == "gameback") {
					GameOut();
				}
				else if (hit.collider.gameObject.tag == "playgame") {
					StartCoroutine(PlayGame());
				}
				lastTag = hit.collider.gameObject.tag;
			}
		}
	}
}
