using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager {
	public static List<Unit> units;

	public static GameObject Canvas;
	private static GameObject ImageButton;
	private static GameObject SmallButton;

	enum State { Base, ClickedUnit, ClickedBehaviour };
	static State state;
	static Unit selectedUnit = null;
	static Magic selectedBehaviour = null;

	public static void InitiateBattle (){
		GetGameManagerInstances ();

		state = State.Base;
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

		StartAllyTurn ();
	}
	private static void GetGameManagerInstances(){
		ImageButton = GameManager.Instance.ImageButton;
		SmallButton = GameManager.Instance.SmallButton;
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

		List<Unit> allyUnits = GetAllyUnits ();

		int allyUnitsNum = allyUnits.Count();

		x = -500;
		y = 250;
		foreach (Unit unit in allyUnits) {
			CreateUnitButton (unit, x, y);
			y -= ySpace;
		}


		List<Unit> enemyUnits = GetEnemyUnits ();

		int enemyUnitsNum = enemyUnits.Count();

		x = 500;
		y = 250;
		foreach (Unit unit in enemyUnits) {
			CreateUnitButton (unit, x, y);
			y -= ySpace;
		}
	}
	private static void CreateUnitButton(Unit unit, int x,int y){
		GameObject button;
		button = Util.CreateButton (ImageButton, Canvas.transform, x, y, null, () => { 
			if(state == State.Base){
				if(unit.GetSide() == Unit.Sides.Ally){
					state = State.ClickedUnit;
					selectedUnit = unit;
					CreateBehaviourButtons(unit, x+230, y); 
				}
			}
			else if(state == State.ClickedBehaviour){
				state = State.Base;
				DestroyBehaviourButtons();
				selectedBehaviour.Cast(selectedUnit, unit);
				EndUnitBehave(selectedUnit);
			}
		} );
		unit.SetUnitButton (button);
		button.GetComponent<Image> ().sprite = unit.GetSprite ();
		unit.CreateHPAndMPStatBars ();
	}

	static List<GameObject> behaviourButtons;

	private static void CreateBehaviourButtons(Unit unit, int x,int y){
		behaviourButtons = new List<GameObject> ();
		int ySpace = 50;
		foreach(Magic magic in unit.GetMagics()) {
			GameObject button;
			button = Util.CreateButton (SmallButton, Canvas.transform, x, y, magic.name, () => {
				state = State.ClickedBehaviour;
				selectedBehaviour = magic;
			});
			behaviourButtons.Add (button);
			y -= ySpace;
		}
	}
	private static void DestroyBehaviourButtons(){
		foreach (var button in behaviourButtons) {
			MonoBehaviour.Destroy (button);
		}
	}

	private static void StartAllyTurn(){	
		List<Unit> allyUnits = GetAllyUnits ();
		foreach (var unit in allyUnits) {
			LetUnitBehave (unit);
		}
	}
	private static bool CheckNoAllyBehaveLeft(){
		bool noAllyLeft = true;
		List<Unit> allyUnits = GetAllyUnits ();
		foreach (var unit in allyUnits) {
			if (unit.GetActivation () == Unit.Activation.Behaveable)
				noAllyLeft = false;
		}
		return noAllyLeft;
	}
	private static void EndAllyTurn(){
		StartAllyTurn ();
	}

	private static void LetUnitBehave(Unit unit){
		unit.SetActivation (Unit.Activation.Behaveable);
		unit.GetUnitButton ().GetComponent<Button> ().interactable = true;
	}
	private static void EndUnitBehave(Unit unit){
		unit.SetActivation (Unit.Activation.AlreadyBehaved);
		unit.GetUnitButton ().GetComponent<Button> ().interactable = false;
		if (CheckNoAllyBehaveLeft ()) {
			EndAllyTurn ();
		}
	}
	private static void DeactivateUnitBehave(Unit unit){
		unit.SetActivation (Unit.Activation.Deactivated);
		unit.GetUnitButton ().GetComponent<Button> ().interactable = false;
	}
	private static List<Unit> GetAllyUnits(){
		IEnumerable<Unit> allyUnits =
			from unit in units
				where unit.GetSide() ==Unit.Sides.Ally
			select unit;
		return allyUnits.ToList();
	}
	private static List<Unit> GetEnemyUnits(){
		IEnumerable<Unit> enemyUnits =
			from unit in units
				where unit.GetSide() ==Unit.Sides.Enemy
			select unit;
		return enemyUnits.ToList();
	}
	private static bool NoAllyLeft(){
		return GetAllyUnits().Count ==  0;
	}
	private static bool NoEnemyLeft(){
		return GetEnemyUnits().Count ==  0;
	}
}
