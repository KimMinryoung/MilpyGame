using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TortureManager {

	public static Dictionary<string, Torture> tortures;
	public static List<Person> prisoners;

	public static GameObject Canvas;
	public static GameObject SmallButton;

	public static void InitiateTorture () {
		prisoners = GameManager.prisoners;
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
