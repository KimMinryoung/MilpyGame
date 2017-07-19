using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue {

	public static DialogueDisplay dd;
	public static DialogueManager dm;

	string label = null;
	Action NameBox = NullNameBox;
	Action PortraitBox = NullPortraitBox;
	Action Text = NullText;
	Action Effect = NullEffect;
	Action Branch = NullBranch;
	Func<bool> Condition = NullCondition;
	Action ChangeValue = NullChangeValue;
	Action DontWaitInput = NullDontWaitInput;

	public void LoadDialogueLine(string line, Dictionary<string,int> comparedVariables){

		try{
			string[] labelParsed = line.Split(']');

			string lineWithoutLabel;

			if(labelParsed.Length==1){
				label = null;
				lineWithoutLabel=labelParsed[0];
			}
			else{
				label = labelParsed[0].Substring("[".Length);
				DontWaitInput = TrueDontWaitInput;
				return;
			}

			string[] parts = lineWithoutLabel.Split('\\');

			if (parts[0] == "*"){
				string commandType = parts[1];
				string commandObject = parts[2];
				if(commandType == "사라져"){
					if(commandObject=="배경"){
						Effect = () => {
							dd.RemoveBackgroundSprite();
						};
					}
					else if(commandObject=="일러"){
						Effect = () => {
							dd.RemoveIllustSprite();
						};
					}
					else if(commandObject=="배경음"){
						//end bgm
					}
				}
				else if(commandType=="배경"){
					Effect = () => {
						Sprite sprite=Resources.Load<Sprite>("Backgrounds/"+commandObject);
						dd.PutBackgroundSprite(sprite);
					};
				}
				else if(commandType=="일러"){
					Effect = () => {
						Sprite sprite=Resources.Load<Sprite>("Illusts/"+commandObject);
						dd.PutIllustSprite(sprite);
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
			else if (parts[0] == "->" || parts[0] == "?"){
				Branch = () => {
					Dialogue line_;
					bool success = false;
					for(int i=0;i<dm.dialogues.Count;i++){
						line_ = dm.dialogues[i];
						if(line_.label == parts[2]){
							dm.lineNum = i;
							success = true;
							break;
						}
					}
					if(!success){
						Debug.Log("label 못 찾아 브랜치 실패");
					}
					dm.ExecutePresentLine();
				};
				if(parts[0] == "?"){
					Condition=()=>{
						string compareText = parts[1];
						string[] tokens = compareText.Split (' ');

						int targetValue = comparedVariables [tokens[0]];
						string compareSymbol = tokens [1];
						int referenceValue = Convert.ToInt32 (tokens [2]);

						bool compareResult = Util.Compare (targetValue, referenceValue, compareSymbol);
						return compareResult;
					};
				}
			}
			else if(parts[0] == "+="){
				string targetStat = parts[1];
				int change = Convert.ToInt32 (parts[2]);
				ChangeValue = () => {
					comparedVariables[targetStat] += change;
				};
			}
			else{
				if(parts[0] .Length != 0){
					NameBox = () =>{
						dd.EnableNameBox();
						dd.PutNameText(parts[0]);
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
					dd.EnableTextBox();
					dd.PutTextText(displayedText);
				};
			}

			if(parts[0] == "*" || parts[0] == "+="){
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
			dd.EnableTextBox();
			dd.PutTextText(line);
		};
	}

	public void ExecuteDialogue(){
		NameBox ();
		PortraitBox ();
		Text ();
		Effect ();
		ConditionAndBranch ();
		ChangeValue ();
		DontWaitInput ();
	}

	private void ConditionAndBranch(){
		bool result = Condition ();
		if (result)
			Branch ();
		else
			DontWaitInput = TrueDontWaitInput;
	}

	private static Action NullNameBox = () => {
		//don't change current name box
		//do nothing
	};
	private static Action EmptyNameBox = () => {
		dd.DisableNameBox();
		dd.PutNameText(null);
	};
	private static Action NullText = () => {
		//don't change current text
		//do nothing
	};
	/*private static Action EmptyText = () => {
		dd.DisableTextBox();
		dd.PutTextText(null);
	};*/
	private static Action NullPortraitBox = () => {
		dd.RemovePortraitSprite();
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
	private static Action NullChangeValue = () => {
		//do nothing
	};
	private static Action NullDontWaitInput = () => {
		//do nothing
	};
	private static Action TrueDontWaitInput = () => {
		dm.ToNextLine();
	};
}
