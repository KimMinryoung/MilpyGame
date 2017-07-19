using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue {

	public static DialogueDisplay dd;
	public static DialogueManager dm;

	private string label = null;
	public Action NameBox = NullNameBox;
	public Action PortraitBox = NullPortraitBox;
	public Action Text = NullText;
	public Action Effect = NullEffect;
	public Action Branch = NullBranch;
	public Func<bool> Condition = NullCondition;
	public Action DontWaitInput = NullDontWaitInput;

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

				if(commandType == "사라져"){
					if(commandObject=="배경"){
						Effect = () => {
							dd.background.sprite = dd.transparentSprite;
						};
					}
					else if(commandObject=="일러"){
						Effect = () => {
							dd.illustObject.GetComponent<Image>().sprite=dd.transparentSprite;
						};
					}
					else if(commandObject=="배경음"){
						//end bgm
					}
				}

				else if(commandType=="배경"){
					Effect = () => {
						Sprite backgroundImage=Resources.Load<Sprite>("Backgrounds/"+commandObject);
						dd.background.sprite = backgroundImage;
					};
				}
				else if(commandType=="일러"){
					Effect = () => {
						Sprite illustImage=Resources.Load<Sprite>("Illusts/"+commandObject);
						dd.illustObject.GetComponent<Image>().sprite=illustImage;
						dd.illustObject.GetComponent<RectTransform> ().sizeDelta = illustImage.rect.size;
					};
				}
				else if(commandType=="배경음"){
					//load bgm
				}
				else if(commandType=="효과음"){
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
						return false;
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
				else{
					NameBox = EmptyNameBox;
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

			if(parts[0] == "*" || parts[0] == "=>" || parts[0] == "?"){
				DontWaitInput = TrueDontWaitInput;
			}

		}
		catch (Exception e){
			Debug.LogError("Parse error with " + line);
			Debug.LogException(e);
			throw e;
		}
	}

	public void LoadMessageLine(string line){
		NameBox = EmptyNameBox;
		Text = () => {
			dd.textBox.enabled = true;
			dd.textText.text = line;
		};
	}

	public void ExecuteDialogue(){
		NameBox ();
		PortraitBox ();
		Text ();
		Effect ();
		ConditionAndBranch ();
		DontWaitInput ();
	}

	private void ConditionAndBranch(){
		bool result = Condition ();
		if (result)
			Branch ();
	}

	private static Action NullNameBox = () => {
		//don't change current name box
		//do nothing
	};
	private static Action EmptyNameBox = () => {
		dd.nameBox.enabled = false;
		dd.nameText.text = null;
	};
	private static Action NullPortraitBox = () => {
		dd.portrait.sprite = dd.transparentSprite;
	};
	private static Action NullText = () => {
		//don't change current text
		//do nothing
	};
	private static Action EmptyText = () => {
		dd.textBox.enabled = false;
		dd.textText.text = null;
	};
	private static Action NullEffect = () => {
		//do nothing
	};
	private static Action NullBranch = () => {
		//do nothing
	};
	private static Func<bool> NullCondition = () => {//this is called for non-conditioned branch
		return true;
	};
	private static Action NullDontWaitInput = () => {
		//do nothing
	};
	private static Action TrueDontWaitInput = () => {
		dm.ToNextLine();
	};
}
