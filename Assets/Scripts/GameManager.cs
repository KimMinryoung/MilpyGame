using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private static GameManager instance;
	public static GameManager Instance {
		get { return instance; }
	}
	public static GameObject Canvas;
	public GameObject SmallButton;

	public static Dictionary<string, int> mainIntegers;
	public static List<Person> prisoners;

	void Awake () {
		instance = this;

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
