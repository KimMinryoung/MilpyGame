using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public static DialogueDisplay dd;

	List<Dialogue> dialogues;
	int lineNum;

	void Awake (){
		dialogues = new List<Dialogue>();
		lineNum = 0;
	}
	void Start(){
	}

	public void LoadDialogueFile(string fileName, string label, Func<string,string> ReplaceWords,Dictionary<string,int> comparedVariables){
		TextAsset dialogueTextAsset = Resources.Load<TextAsset> ("Texts/" + fileName);
		string dialoguesString = dialogueTextAsset.text;

		if (label != "") {
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
			dialogue.LoadDialogueLine (line);
			dialogues.Add (dialogue);
		}

		dialogues [0].ExecuteDialogue ();
	}

	private void DialoguesClear(){
		dialogues.Clear ();
		lineNum = 0;
	}

	void ToNextLine(){
		lineNum++;
		if (lineNum >= dialogues.Count) {
			DialoguesClear ();
			dd.DialogueDisplayClear ();
			return;
		}
		dialogues [lineNum].ExecuteDialogue ();
	}
	void Update(){
		if ( Input.GetMouseButtonDown(1) ) {
			LoadDialogueFile ("opening_scene_texts", "", NoReplace, null);
		}
		if ( Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) ) {
			ToNextLine ();
		}
	}

	private static Func<string, string> NoReplace = (string a) => {
		return a;
	};
}
