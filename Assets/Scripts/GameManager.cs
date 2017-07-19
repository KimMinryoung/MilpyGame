using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static List<Person> prisoners;

	public static GameManager gameManager;
	public static GameObject Canvas;
	public GameObject SmallButton;

	void Awake () {
		gameManager = this;

		//dialogueManager = GameObject.Find ("DialogueManager").GetComponent<DialogueManager> ();
		//battleManager = GameObject.Find ("GameManager").GetComponent<BattleManager> ();

		Canvas = GameObject.FindGameObjectWithTag ("Canvas");
		TortureManager.Canvas = Canvas;
	}

	void Start(){
		prisoners = new List<Person> ();
		prisoners.Add (new Person ("asdf"));
		TortureManager.InitiateTorture ();
	}
}
