using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DG.Tweening;

public class ScriptingCamera1 : MonoBehaviour {
	public GameObject Alien1;
	public GameObject Alien2;
	public GameObject Ufo;
	public Text dialogText;
	public int levelNum = 1;

	private string[] lines1 = {"Just outside Pluto’s orbit",
"Finally! We have arrived in Human space.",
"The fleet will dine well tonight overbrain",
"Not tonight, first the humans must learn fear!",
"How will we teach them fear?! Your brainnificences? Disintegration?",
"Like a great man said, “No, disintegration!”",
"Then how?",
"By moving slowly back and forth and ramming into their ships!"
};
	private string[] lines2 = {"Near Pluto",
"Sir, might it be possible to attack the humans by, say, shooting at them?",
"NO!",
"Why not?",
"I want them to learn fear. They will know that even with an unnecessary and ridiculous self imposed handicap we still annihilated them!",
"But magnificent alpha brain, wouldn’t it be…",
"Silence! Go out there and make yourself a sitting duck.",
"Yes sir"
};
	private string[] lines3 = {"Getting pretty darn close to Pluto",
"Mighty brainzilla, this invasion is not going as well as it could.",
"You’re right, the humans need extra fear!",
"YES! Lasers will bring them fear, I will pass the order",
"NO! Instead, tell our best soldiers to strap screen clearing bombs onto themselves?",
"Why?",
"So that the humans will know that we are so superior we will carry megabombs that only kill aliens just to taunt them!",
"And if they shoot one?",
"Everyone dies. ATTACK!"
};
	private string[] lines4 = {"A little farther away from Pluto (A map would have been useful)",
"Where did Alien 438297 go?",
"He died in a mega bomb related accident.",
"Good! He was a whiner",
"Sir, the Humans radioed asking our intentions.",
"Our intentions?",
"Yes…",
"Our intentions are to descend on them as locusts crush their pathetic “Democracy” under our mighty boot, throw their disgusting “statue of liberty” into the ocean, suck the brains out of their very heads, and fill rivers with tears!",
"I’ll just say “We come in peace”"
};
	private string[] lines5 = {"Where Charon used to be",
"Ha! That little moon was no match for us!",
"No sir!",
"In fact you could say it’s Chared-on now!",
"Ha!",
"I find your lack of laughter disturbing.",
"Ha ha ha!",
"That wasn’t a joke.",
"Uhhhh",
"I have a special mission for you!"
};
	private string[] lines6 = {"So close to Pluto you can almost taste it",
"I seem to have killed all of my advisors.",
"How am I supposed to plan a glorious invasion with a fleet full of cowards!",
"Oh well, forward to victory!",
"I mean, side to side to victory!"
};
	private string[] lines8 = {"Totally, like at Pluto-ish",
"Booooooooooooooooooooooooooooored"};
	private string[] lines7 = {"Right above Pluto, I mean, like, right there",
"Most glorious and might brain of 10 trillion neurons!",
"I like you",
"The humans are trying to negotiate peace",
"Which piece? I like dark meat…",
"No sir, I mean an end to this war.",
"They insult me! No one negotiates peace with me…",
"This means war!"

};
	private string[] lines9 = {"Finally at Pluto!",
"I’m coming this time!",
"But sir…",
"None of that! I’m bored!",
"But,",
"I intend put the entire invasion at risk because I want some action!",
"Brain GO!",
"Is that your catchphrase?",
"Doesn’t work?",
"No",
"Go go power brainer!"
	};

	public AudioClip[] clips;
	AudioSource audio1;
	int activeAlien = 0;
	Vector3 initialCamera;
	
	void goToAlien1(float time){
		activeAlien = 0;
		transform.DOMove(new Vector3(Alien1.transform.position.x,Alien1.transform.position.y+.05f,Alien1.transform.position.z-0.5f),time);
	}
	void goToAlien2(float time){
		activeAlien = 1;
		transform.DOMove(new Vector3(Alien2.transform.position.x,Alien2.transform.position.y+.05f,Alien2.transform.position.z-0.5f),time);
	}
	void showDialog(int linenum){
		switch (levelNum)
		{
			case 1: dialogText.text = lines1[linenum];break;
			case 2: dialogText.text = lines2[linenum];break;
			case 3: dialogText.text = lines3[linenum];break;
			case 4: dialogText.text = lines4[linenum];break;
			case 5: dialogText.text = lines5[linenum];break;
			case 6: dialogText.text = lines6[linenum];break;
			case 7: dialogText.text = lines7[linenum];break;
			case 8: dialogText.text = lines8[linenum];break;
			case 9: dialogText.text = lines9[linenum];break;
			default: dialogText.text = lines1[linenum];
			break;
		}
		audio1.PlayOneShot(clips[linenum]);
		if (activeAlien == 0)
			Alien1.transform.DOShakeRotation(clips[linenum].length,10,2);
		else
			Alien2.transform.DOShakeRotation(clips[linenum].length,10,2);
	}
	public void nextLevel(){
					Application.LoadLevel(levelNum+9);
	}
	public void EndAnimation() {
		dialogText.text="";
		transform.DOMove(initialCamera,1.5f);
		Ufo.transform.DOMoveX(50,4.0f).SetEase(Ease.Linear).OnComplete(()=>{
			//int level = levelNum;//PlayerPrefs.GetInt("level",1);
			//level += 9;
			//PlayerPrefs.SetInt("level",level);
			Application.LoadLevel(levelNum+9);
		});		
	}
	
	// Use this for initialization
	void Start () {
		initialCamera = transform.position;
		audio1 = GetComponent<AudioSource>();
			switch (levelNum)
		{
			case 1: StartCoroutine( L1());break;
			case 2: StartCoroutine( L2());break;
			case 3: StartCoroutine( L3());break;
			case 4: StartCoroutine( L4());break;
			case 5: StartCoroutine( L5());break;
			case 6: StartCoroutine( L6());break;
			case 7: StartCoroutine( L7());break;
			case 8: StartCoroutine( L8());break;
			case 9: StartCoroutine( L9());break;
		}
}
	IEnumerator L1(){
		dialogText.text = lines1[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,3.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(3.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			EndAnimation();
	}
	IEnumerator L2(){
		dialogText.text = lines2[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,3.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(3.0f);
			goToAlien2(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			EndAnimation();
	}
	IEnumerator L3(){
		dialogText.text = lines3[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,3.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(3.0f);
			goToAlien2(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(8);
		yield return new WaitForSeconds(clips[8].length);
			EndAnimation();
	}
	IEnumerator L4(){
		dialogText.text = lines4[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,4.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(4.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(8);
		yield return new WaitForSeconds(clips[8].length);
			EndAnimation();
	}
	IEnumerator L5(){
		dialogText.text = lines5[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,4.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(4.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(8);
		yield return new WaitForSeconds(clips[8].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(9);
		yield return new WaitForSeconds(clips[9].length);
			EndAnimation();
	}
	IEnumerator L6(){
		dialogText.text = lines6[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,4.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(4.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			EndAnimation();
	}
	IEnumerator L7(){
		dialogText.text = lines7[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,4.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(4.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			EndAnimation();
	}
	IEnumerator L8(){
			dialogText.text = lines8[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,4.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(4.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(8);
		yield return new WaitForSeconds(clips[8].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(9);
		yield return new WaitForSeconds(clips[9].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(10);
		yield return new WaitForSeconds(clips[10].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(11);
		yield return new WaitForSeconds(clips[11].length);
				EndAnimation();
	}
	IEnumerator L9(){
		dialogText.text = lines9[0];
		audio1.PlayOneShot(clips[0]);
		Ufo.transform.DOMoveX(0,4.0f).SetEase(Ease.OutCirc);
		yield return new WaitForSeconds(4.0f);
			goToAlien1(2.0f);
		yield return new WaitForSeconds(2.0f);
			showDialog(1);
		yield return new WaitForSeconds(clips[1].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(2);
		yield return new WaitForSeconds(clips[2].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(3);
		yield return new WaitForSeconds(clips[3].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(4);
		yield return new WaitForSeconds(clips[4].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(5);
		yield return new WaitForSeconds(clips[5].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(6);
		yield return new WaitForSeconds(clips[6].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(7);
		yield return new WaitForSeconds(clips[7].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(8);
		yield return new WaitForSeconds(clips[8].length);
			goToAlien2(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(9);
		yield return new WaitForSeconds(clips[9].length);
			goToAlien1(0.5f);
		yield return new WaitForSeconds(0.5f);
			showDialog(10);
		yield return new WaitForSeconds(clips[10].length);
				EndAnimation();
	}

	// Update is called once per frame
	void Update () {
		//if (Input.GetMouseButtonDown(0)) {
		//	DOTween.CompleteAll();
		//}
	}
}
