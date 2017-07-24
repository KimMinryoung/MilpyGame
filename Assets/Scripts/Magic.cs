using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic{
	public static Dictionary<string, Magic> magics;
	public string name;
	private static Action<Unit> NullConsumeMP = (unit) => {};
	Action<Unit> ConsumeMP = NullConsumeMP;
	private static Func<Unit, Unit, int> NullGetDamage = (caster, target) => { return 0; };
	Func<Unit, Unit, int> GetDamage = NullGetDamage;
	private static Action<Unit, int> NullDoTheDamage = (target, damage) => {};
	Action<Unit, int> DoTheDamage = NullDoTheDamage;

	public static void CreateAllMagics(){
		magics = new Dictionary<string, Magic> ();
		magics["평타"] = new Magic("평타",0, 2.0);
		magics["강타"] = new Magic("강타",30,5.0);
	}

	public Magic(string name, int MPconsumption, double powerFactor){
		this.name = name;
		ConsumeMP = (Unit caster) => {
			Dictionary<string, int> MPChange=new Dictionary<string, int>();
			MPChange["MP"]=-MPconsumption;
			caster.ChangeStatsAndAddMessages(MPChange);
		};
		GetDamage = (Unit caster, Unit target) => {
			double power = powerFactor * caster.GetStat ("마법력");
			int damage = (int)Util.Max (1, power - target.GetStat ("저항력"));
			return damage;
		};
		DoTheDamage = (Unit target, int damage) => {
			Dictionary<string, int> changeStats = new Dictionary<string, int> ();
			changeStats.Add ("HP", -damage);
			target.ChangeStatsAndAddMessages (changeStats);
		};
	}
	public void Cast(Unit caster, Unit target){
		ConsumeMP (caster);
		int damage = GetDamage (caster, target);
		DoTheDamage (target, damage);
	}
}