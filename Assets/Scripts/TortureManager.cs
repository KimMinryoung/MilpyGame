﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TortureManager {

	public static Dictionary<string, Torture> tortures;
	public static Dictionary<string, Person> prisoners;

	public static GameObject Canvas;
	private static GameObject SmallButton;

	public static void InitiateTorture () {
		GetGameManagerInstances ();
		LoadTortureData ();
		CreateTortureButtons ();
	}
	private static void GetGameManagerInstances(){
		prisoners = GameManager.prisoners;
		SmallButton = GameManager.Instance.SmallButton;
	}
	private static void LoadTortureData(){
		TextAsset textDataFile = Resources.Load<TextAsset>("Texts/"+"torture_texts");
		string textDataString = textDataFile.text;
		string[] textDataLines = textDataString.Split (new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
		tortures = new Dictionary<string, Torture> ();
		foreach(string line in textDataLines) {
			string[] textEntry = line.Split (',');
			Torture torture = new Torture (textEntry);
			tortures [torture.name] = torture;
		}
	}
	private static void CreateTortureButtons(){
		int x = -300;
		int y = 200;
		int ySpace = 50;
		foreach(var pair in tortures) {
			GameObject button;
			button = Util.CreateButton (SmallButton, Canvas.transform, x, y, pair.Key, () => pair.Value.tortureAction (prisoners ["바올리"]));
			y -= ySpace;
		}
	}
}
