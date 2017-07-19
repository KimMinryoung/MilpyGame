using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public static DialogueDisplay dd;

	public List<Dialogue> dialogues;
	public int lineNum;

	Dictionary<string, int> emptyCV=new Dictionary<string,int>();

	void Awake (){
		Dialogue.dm = this;
		Person.dm = this;

		dialogues = new List<Dialogue>();
		lineNum = 0;
	}

	void Start(){
	}

	void Update(){
		if ( Input.GetMouseButtonDown(1) ) {
			LoadDialogueFile ("opening_scene_texts", null, NoReplace, emptyCV);
		}
		if ( DuringDialogue() && ( Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) ) ) {
			ToNextLine ();
		}
	}

	public void LoadDialogueFile(string fileName, string label, Func<string,string> ReplaceWords, Dictionary<string,int> comparedVariables){
		TextAsset dialogueTextAsset = Resources.Load<TextAsset> ("Texts/" + fileName);
		string dialoguesString = dialogueTextAsset.text;

		if (label != null) {
			label = "{" + label + "}";
			string[] parts = dialoguesString.Split (new string[] { label }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length < 2) {
				Debug.Log ("다이얼로그 파일 로드 중 label 오류");
				return;
			}
			dialoguesString = dialoguesString.Split (new string[] { label }, StringSplitOptions.RemoveEmptyEntries) [1];
		}

		dialoguesString = ReplaceWords (dialoguesString);

		string[] dialogueLines = dialoguesString.Split (new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
		Dialogue dialogue;
		foreach(string line in dialogueLines) {
			dialogue = new Dialogue ();
			dialogue.LoadDialogueLine (line, comparedVariables);
			dialogues.Add (dialogue);
		}

		if(lineNum == 0)
			ExecutePresentLine();
	}

	public void LoadMessageLine(string line){
		Dialogue dialogue = new Dialogue ();
		dialogue.LoadMessageLine (line);
		dialogues.Add (dialogue);
	}

	private void DialoguesClear(){
		dialogues.Clear ();
		lineNum = 0;
	}

	public void ToNextLine(){
		lineNum++;
		if (lineNum >= dialogues.Count) {
			DialoguesClear ();
			dd.DialogueDisplayClear ();
			return;
		}
		ExecutePresentLine ();
	}
	public void ExecutePresentLine(){
		dialogues [lineNum].ExecuteDialogue ();
	}

	public bool DuringDialogue(){
		return dialogues.Count != 0;
	}

	private static Func<string, string> NoReplace = (a => a);
}
