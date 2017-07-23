using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager {
	public static Dictionary<string, Unit> units;

	public static GameObject Canvas;
	private static GameObject ImageButton;

	public static void InitiateBattle (){
		GetGameManagerInstances ();
		LoadUnits ();
		PutBackground ();
		CreateUnitButtons ();
	}
	private static void GetGameManagerInstances(){
		ImageButton = GameManager.Instance.ImageButton;
	}
	private static void LoadUnits(){
		units = new Dictionary<string, Unit> ();
		foreach (var pair in GameManager.persons) {
			units [pair.Key] = new Unit (pair.Value);
		}
	}
	private static void PutBackground(){
		DialogueDisplay.Instance.PutBackgroundSprite ("Blue");
	}
	private static void CreateUnitButtons(){
		int x = -500;
		int y = 250;
		int ySpace = 200;
		foreach(var pair in units) {
			Unit unit = pair.Value;
			GameObject button = MonoBehaviour.Instantiate (ImageButton,Canvas.transform);
			button.transform.Translate (new Vector3 (x, y, 0));
			unit.SetUnitButton (button);
			button.GetComponent<Image> ().sprite = pair.Value.GetSprite ();
			y -= ySpace;

			unit.CreateHPAndMPStatBars ();
			//button.GetComponent<Button>().onClick.AddListener(() => pair.Value.tortureAction(prisoners["바올리"]));
		}
	}
	void Update () {
		
	}
}
