using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torture {
	public string name;
	private Dictionary<string,int> statChanges;
	public Action<Person> tortureAction;

	public Torture(string[] textEntry){
		name=textEntry[0];
		statChanges=new Dictionary<string,int>();
		int i;
		for(i=1;i<textEntry.Length;i+=2){
			statChanges.Add(textEntry[i],Convert.ToInt32(textEntry[i+1]));
		}
		tortureAction = (Person subject) => {
			if (Dialogue.dm.DuringDialogue())
				return;
			//string fileName, string label, Func<string,string> ReplaceWords, Dictionary<string,int> comparedVariables
			Func<string,string> Replace = (origin)=>{ return origin.Replace("{이름}",subject.GetName()); };
			Dialogue.dm.LoadDialogueFile("torture_dialogue_texts", name, Replace, subject.GetStats());
			subject.ChangeStats (statChanges);
		};
	}
	/*
		public static GameObject Canvas;
		public static GameObject SmallButton;

		public static void InitiateTorture () {
			prisoners = GameManager.mainVari.prisoners;
			Canvas = GameManager.Canvas;
			SmallButton = GameManager.gameManager.SmallButton;

			TextAsset textDataFile = Resources.Load<TextAsset>("Texts/"+"torture_texts");
			string textDataString = textDataFile.text;
			string[] textDataLines = textDataString.Split (new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);

			tortures = new Dictionary<string, Torture> ();
			foreach(string line in textDataLines) {
				string[] textEntry = line.Split (',');
				Torture torture = new Torture (textEntry);
				tortures [torture.name] = torture;
			}

			int y = 600;
			foreach(var pair in tortures) {
				GameObject button = MonoBehaviour.Instantiate (SmallButton,new Vector3(300,y,0),Quaternion.identity,Canvas.transform);
				y -= 50;
				button.transform.Find ("SmallButtonText").GetComponent<Text> ().text = pair.Key;
				button.GetComponent<Button>().onClick.AddListener(() => pair.Value.tortureAction(prisoners[0]));
			}
		}
	}
	*/
}
