using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager {
	public static List<Unit> units;

	public static GameObject Canvas;
	private static GameObject ImageButton;

	public static void InitiateBattle (){
		GetGameManagerInstances ();

		units = new List<Unit> ();
		List<string> allyUnits = new List<string> ();
		List<string> enemyUnits = new List<string> ();
		allyUnits.Add ("젠");
		allyUnits.Add ("아이리스");
		LoadUnitsOfSides (allyUnits, Unit.Sides.Ally);
		enemyUnits.Add ("밀피");
		LoadUnitsOfSides (enemyUnits, Unit.Sides.Enemy);
		PutBackground ();
		CreateUnitButtons ();
	}
	private static void GetGameManagerInstances(){
		ImageButton = GameManager.Instance.ImageButton;
	}
	private static void LoadUnitsOfSides(List<string> unitNames, Unit.Sides side){
		foreach (string name in unitNames) {
			Unit unit = new Unit (GameManager.persons [name]);
			units.Add (unit);
			unit.SetSide (side);
		}
	}
	private static void PutBackground(){
		DialogueDisplay.Instance.PutBackgroundSprite ("Blue");
	}
	private static void CreateUnitButtons(){
		int x, y;
		int ySpace = 200;

		IEnumerable<Unit> allyUnits =
			from unit in units
				where unit.GetSide() ==Unit.Sides.Ally
			select unit;

		int allyUnitsNum = allyUnits.Count();

		x = -500;
		y = 250;
		foreach (Unit unit in allyUnits) {
			CreateUnitButton (unit, x, y);
			y -= ySpace;
		}


		IEnumerable<Unit> enemyUnits =
			from unit in units
				where unit.GetSide() ==Unit.Sides.Enemy
			select unit;

		int enemyUnitsNum = enemyUnits.Count();

		x = 500;
		y = 250;
		foreach (Unit unit in enemyUnits) {
			CreateUnitButton (unit, x, y);
			y -= ySpace;
		}
	}
	private static void CreateUnitButton(Unit unit, int x,int y){
		GameObject button = MonoBehaviour.Instantiate (ImageButton,Canvas.transform);
		button.transform.Translate (new Vector3 (x, y, 0));
		unit.SetUnitButton (button);
		button.GetComponent<Image> ().sprite = unit.GetSprite ();

		unit.CreateHPAndMPStatBars ();
		button.GetComponent<Button>().onClick.AddListener(() => unit.GetMagics()[0].Cast(unit, unit) );
	}
	void Update () {
		
	}
}
