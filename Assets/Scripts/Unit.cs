using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : Person{
	Person person;
	public enum Sides { Ally, Enemy };
	Sides side;
	public enum Activation { Behaveable, AlreadyBehaved, Deactivated };
	Activation activation;
	private GameObject unitButton;
	private static GameObject StatBar;

	public Unit(Person person){
		this.person = person;
		this.name = person.GetName();
		AddStat ("HP", 10 * person.GetStat ("체력"), 0, 10 * person.GetStat ("체력"));
		AddStat ("MP", 10 * person.GetStat ("마력"), 0, 10 * person.GetStat ("마력"));
		AddStat ("마법력", 1000, 1, person.GetStat ("마력"));
		AddStat ("마법기술", 1000, 1, person.GetStat ("지능"));
		AddStat ("저항력", 1000, 1, person.GetStat ("정신"));
		AddStat ("순발력", 1000, 1, person.GetStat ("민첩"));
		AddStat ("운", 100, 1, person.GetStat ("행운"));

		magics = person.GetMagics();

		StatBars=new Dictionary<string, Slider>();
	}

	public void CastMagic(Magic magic, Unit target){
		magic.Cast (this, target);
	}

	public static void GetGameManagerInstances(){
		StatBar = GameManager.Instance.StatBar;
	}
	public void SetUnitButton(GameObject unitButton){
		this.unitButton = unitButton;
	}
	public GameObject GetUnitButton(){
		return unitButton;
	}
	public Sides GetSide(){
		return side;
	}
	public void SetSide(Sides side){
		this.side = side;
	}
	public Activation GetActivation(){
		return activation;
	}
	public void SetActivation(Activation activation){
		this.activation = activation;
	}
	new public string GetName(){
		return name;
	}
	new public List<Magic> GetMagics(){
		return magics;
	}
	public void CreateHPAndMPStatBars (){
		List<string> statNames = new List<string> ();
		statNames.Add ("HP");
		statNames.Add ("MP");
		CreateStatBars (statNames);
	}
	private void CreateStatBars (List<string> statNames) {
		//DestroyStatBars ();
		int y = 100;
		foreach(string statName in statNames){
			CreateStatBar (statName, y);
			y -= 30;
		}
	}
	private void CreateStatBar(string name,  int y){
		GameObject statBarObject = MonoBehaviour.Instantiate (StatBar,unitButton.transform);
		statBarObject.transform.Translate(new Vector3 (0, y, 0));
		Slider statBar = statBarObject.GetComponent<Slider>();
		StatBars[name] = statBar;
		SetStatBarValue(statBar, name);
		statBarObject.transform.Find ("Text").GetComponent<Text>().text = name;
	}
	/*
	public void DestroyStatBars () {
		if (StatBars == null)
			return;
		foreach (var statBar in StatBars) {
			MonoBehaviour.Destroy (statBar);
		}
		StatBars.Clear ();
	}*/
}
