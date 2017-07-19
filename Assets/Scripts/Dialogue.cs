using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public static DialogueDisplay dd;

	private string label = null;
	public Action NameBox = NullNameBox;
	public Action PortraitBox = NullPortraitBox;
	public Action Text = NullText;
	public Action Effect = NullEffect;
	public Action Branch = NullBranch;
	public Func<bool> Condition = NullCondition;

	public void LoadDialogueLine(string line){
		try{
			string[] labelParsed = line.Split(']');

			string lineWithoutLabel;

			if(labelParsed.Length==1){
				label = null;
				lineWithoutLabel=labelParsed[0];
			}
			else{
				label = labelParsed[0].Substring("[".Length);
				lineWithoutLabel=labelParsed[1];
			}

			string[] parts = lineWithoutLabel.Split('\\');

			if (parts[0] == "*"){
				
				string commandType = parts[1];
				string commandObject = parts[2];
				if(commandType == "disappear"){
					//remove object
				}
				else if(commandType=="bg"){
					Effect = () => {
						Sprite backgroundImage=Resources.Load<Sprite>("Backgrounds/"+commandObject);
						dd.background.sprite = backgroundImage;
					};
				}
				else if(commandType=="illust"){
					Effect = () => {
						Sprite illustImage=Resources.Load<Sprite>("Illusts/"+commandObject);
						dd.illustObject.GetComponent<Image>().sprite=illustImage;
						dd.illustObject.GetComponent<RectTransform> ().sizeDelta = illustImage.rect.size;
					};
				}
				else if(commandType=="bgm"){
					//load bgm
				}
				else if(commandType=="se"){
					//load sound effect
				}
				else{
					Debug.LogError("undefined effectType : " + parts[1]);
				}

			}
			else if (parts[0] == "=>" || parts[0] == "?"){
				Branch = () => {
					// find parts[2] and jump to there
				};
				if(parts[0]=="?"){
					Condition=()=>{
						//check condition using received variables list
						return true;
					};
				}
			}
			else{
				if(parts[0] != ""){
					NameBox = () =>{
						dd.nameBox.enabled = true;
						dd.nameText.text = parts[0];
					};
				}

				//emotion = stringList[1];
				//load portrait

				string displayedText = "";
				string[] textLines=parts[2].Split('|');
				foreach(string textline in textLines){
					displayedText += textline+"\n";
				}
				displayedText = displayedText.Substring(0,displayedText.Length-1);
				Text = () => {
					dd.textBox.enabled = true;
					dd.textText.text = displayedText;
				};
			}
		}
		catch (Exception e){
			Debug.LogError("Parse error with " + line);
			Debug.LogException(e);
			throw e;
		}
	}

	public void LoadMessageLine(string line){
		dd.textBox.enabled = true;
		dd.textText.text = line;
	}

	public void ExecuteDialogue(){
		NameBox ();
		PortraitBox ();
		Text ();
		Effect ();
		ConditionAndBranch ();
	}

	private void ConditionAndBranch(){
		bool result = Condition ();
		if (result)
			Branch ();
	}

	private static Action NullNameBox = () => {
		
		//no name text, no name box image
	};

	public static Action NullPortraitBox = () => {
		//portrait=transparent
	};

	public static Action NullText = () => {
		//don't change current text
	};

	public static Action NullEffect = () => {
		//do nothing
	};

	public static Action NullBranch = () => {
		//do nothing
	};

	public static Func<bool> NullCondition = () => {//this is called for non-conditioned branch
		return true;
	};

}
